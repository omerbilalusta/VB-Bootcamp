using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Vb_Base.Response;
using Vb_Data.UnitOfWork;
using Vb_DTO;
using Vb_Operation.Cqrs;

namespace Vb_Bootcamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IMediator mediator;

        public TokenController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ApiResponse<TokenResponse>> GetToken([FromBody] TokenRequest request)
        {
            var operation = new CreateTokenCommandCompany(request);
            var result = await mediator.Send(operation);
            if(result.Success == false)
            {
                var operation2 = new CreateTokenCommandDealer(request);
                var result2 = await mediator.Send(operation2);
                if (!result2.Success == false)
                    return result2;
            }
            return result;
        }

        [HttpGet("tokenTest")]
        [Authorize]
        public bool TokenTest()
        {
            return true;
        }
    }
}
