using MediatR;
using Vb_Base.Response;
using Vb_DTO;

namespace Vb_Operation.Cqrs
{
    public record CreateTokenCommandDealer(TokenRequest model) : IRequest<ApiResponse<TokenResponse>>;
    public record CreateTokenCommandCompany(TokenRequest model) : IRequest<ApiResponse<TokenResponse>>;
}
