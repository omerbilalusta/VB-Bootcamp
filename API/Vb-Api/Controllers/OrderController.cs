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
    public class OrderController : ControllerBase
    {
        private IMediator mediator;

        public OrderController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("GetAllOrders")]
        public async Task<ApiResponse<List<OrderResponse>>> GetAllOrders()
        {
            var operation = new GetAllOrdersQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetOrderByOrderNumber")]
        public async Task<ApiResponse<OrderResponse>> GetOrderByOrderNumber([FromQuery] int Id)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new GetOrderByOrderNumberQuery(Id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetOrdersByCompanyDealer")]
        public async Task<ApiResponse<List<OrderResponse>>> GetOrdersByCompany()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new GetOrderByCompanyDealerQuery(int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetDeclinedOrders")]
        public async Task<ApiResponse<List<OrderResponse>>> GetDeclinedOrders()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new GetDeclinedOrders(int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<OrderResponse>> Post([FromBody] OrderRequest request)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new CreateOrderCommand(request, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut]
        public async Task<ApiResponse> Put([FromQuery] int id, [FromBody] OrderRequest request)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new UpdateOrderCommand(request, id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete]
        public async Task<ApiResponse> Delete([FromQuery]int orderNumber)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new DeleteOrderCommand(orderNumber, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
