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

        [HttpGet("{id}")]
        public async Task<ApiResponse<OrderResponse>> GetOrderById(int Id)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new GetOrderByIdQuery(Id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetOrdersByCompany")]
        public async Task<ApiResponse<List<OrderResponse>>> GetOrdersByCompany()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new GetOrderByCompanyQuery(int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetOrdersByDealer")]
        public async Task<ApiResponse<List<OrderResponse>>> GetOrdersByDealer()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new GetOrderByDealerQuery(int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("GetDeclinedOrdersById")]
        public async Task<ApiResponse<List<OrderResponse>>> GetDeclinedOrdersById()
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

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] OrderRequest request)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new UpdateOrderCommand(request, id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new DeleteOrderCommand(id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
