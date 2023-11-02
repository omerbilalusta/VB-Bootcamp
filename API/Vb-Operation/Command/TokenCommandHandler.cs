using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vb_Base.Encryption;
using Vb_Base.Model;
using Vb_Base.Response;
using Vb_Base.Token;
using Vb_Data.UnitOfWork;
using Vb_DTO;
using Vb_Operation.Cqrs;

namespace Vb_Operation.Command
{
    public class TokenCommandHandler :
        IRequestHandler<CreateTokenCommandDealer, ApiResponse<TokenResponse>>,
        IRequestHandler<CreateTokenCommandCompany, ApiResponse<TokenResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly JwtConfig jwtConfig;

        public TokenCommandHandler(IUnitOfWork unitOfWork, IOptionsMonitor<JwtConfig> jwtConfig)
        {
            this.unitOfWork = unitOfWork;
            this.jwtConfig = jwtConfig.CurrentValue;
        }

        public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommandDealer request, CancellationToken cancellationToken)
        {   
            var entityDealer = unitOfWork.DealerRepository.GetAsQueryable().Where(x => x.Email == request.model.Email).FirstOrDefault();
            
            if(!Check(entityDealer, request.model))
                return new ApiResponse<TokenResponse>("Invalid user information");

            return ResponseCreate(entityDealer);
        }

        public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommandCompany request, CancellationToken cancellationToken)
        {
            var entityCompany = unitOfWork.CompanyRepository.GetAsQueryable().Where(x => x.Email == request.model.Email).FirstOrDefault(); ;

            if (!Check(entityCompany, request.model))
                return new ApiResponse<TokenResponse>("Invalid user information");

            return ResponseCreate(entityCompany);
        }

        private ApiResponse<TokenResponse> ResponseCreate(UserModel user)
        {
            string token = Token(user);
            TokenResponse tokenResponse_dealer = new()
            {
                Token = token,
                ExpireDate = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                Email = user.Email,
                Role = user.Role,
                Id = user.Id
            };

            return new ApiResponse<TokenResponse>(tokenResponse_dealer);
        }

        private bool Check(UserModel user, TokenRequest model)
        {
            if (user == null)
                return false;

            if (user.IsActive == false)
                return false;

            var sha256_dealer = Sha256.Create(model.Password);
            if (user.Password != sha256_dealer)
                return false;

            return true;
        }

        private string Token(UserModel user)
        {
            Claim[] claims = GetClaims(user);
            var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

            var jwtToken = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
            );

            string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }


        private Claim[] GetClaims(UserModel user)
        {
            var claims = new[]
            {
            new Claim("Id", user.Id.ToString()),
            new Claim("Email", user.Email),
            new Claim("Role", user.Role),
            new Claim(ClaimTypes.Role, user.Role),
        };

            return claims;
        }
    }
}
