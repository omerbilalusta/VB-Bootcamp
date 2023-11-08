using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vb_Base.Response;
using Vb_DTO;
using Vb_Operation.Cqrs;

namespace Vb_Bootcamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private IMediator mediator;

        public OrderDetailController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles ="admin, dealer")]
        [HttpGet]
        public async Task<ApiResponse<List<OrderDetailResponse>>> GetAllOrderDetails()
        {
            var operation = new GetAllOrderDetailQuery();
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
