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
    public class DealerServiceController : ControllerBase
    {
        private IMediator mediator;

        public DealerServiceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        
        [HttpPost]
        public async Task<ApiResponse<DealerResponseShort>> Post([FromBody] DealerServiceRequest request)
        {
            var operation = new CreateDealerServiceCommand(request, 0);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
