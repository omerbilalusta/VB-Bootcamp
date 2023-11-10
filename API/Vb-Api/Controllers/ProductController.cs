using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Vb_Base.Response;
using Vb_DTO;
using Vb_Operation.Cqrs;

namespace Vb_Bootcamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        
        [HttpGet("GetAllProducts")]
        [Authorize(Roles = "admin,dealer")]
        public async Task<ApiResponse<List<ProductResponse>>> GetAllProducts()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var userRole = (User.Identity as ClaimsIdentity).FindFirst("Role").Value;
            var operation = new GetAllProductQuery(int.Parse(userId), userRole);
            var result = await mediator.Send(operation);
            return result;
        }

        
        [HttpGet("GetProdutsById")]
        [Authorize(Roles = "admin,dealer")]
        public async Task<ApiResponse<ProductResponse>> GetProdutsById([FromQuery] int Id)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new GetProductByIdQuery(Id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ApiResponse<ProductResponse>> CreateProduct([FromBody] ProductRequest request)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new CreateProductCommand(request, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ApiResponse> UpdateProduct([FromBody] ProductRequest request, int Id)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new UpdateProductCommand(request, Id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [Authorize(Roles = "admin")]
        [HttpPut("UpdateStock")]
        public async Task<ApiResponse> UpdateProductStock([FromBody] ProductRequest2 request, int Id)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new UpdateProductStockCommand(request, Id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<ApiResponse> DeleteProduct([FromQuery] int Id)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new DeleteProductCommand(Id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [Authorize(Roles = "admin,dealer")]
        [HttpGet("filter")]
        public async Task<ApiResponse<List<ProductResponse>>> GetProductByFilter([FromQuery] int? Id)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new GetProductByFilterQuery(int.Parse(userId), Id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
