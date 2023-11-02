using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vb_Base.Response;
using Vb_DTO;
using Vb_Operation.Cqrs;

namespace Vb_Bootcamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerController : ControllerBase
    {
        private IMediator mediator;

        public DealerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetAllDealer")]
        public async Task<ApiResponse<List<DealerResponse>>> GetAllDealer()
        {
            var operation = new GetAllDealerQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetDealerById")]
        public async Task<ApiResponse<DealerResponse>> GetDealerById([FromQuery] int id)
        {
            var operation = new GetDealerByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ApiResponse<DealerResponse>> Post([FromBody] DealerRequest request)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new CreateDealerCommand(request, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [Authorize(Roles = "admin")]
        [HttpPut("dealerUpdate")]
        public async Task<ApiResponse> PutShort([FromQuery] int id, [FromBody] DealerRequestShort request)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new UpdateDealerShortCommand(request, id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ApiResponse> Put([FromQuery] int id, [FromBody] DealerRequest request)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new UpdateDealerCommand(request, id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<ApiResponse> Delete([FromQuery] int id)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new DeleteDealerCommand(id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
