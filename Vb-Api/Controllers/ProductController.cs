using MediatR;
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

        [HttpGet]
        public async Task<ApiResponse<List<ProductResponse>>> GetAllProducts()
        {
            var operation = new GetAllProductQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<ProductResponse>> GetProdutsById(int Id)
        {
            var operation = new GetProductByIdQuery(Id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<ProductResponse>> CreateProduct([FromBody] ProductRequest request)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new CreateProductCommand(request, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> UpdateProduct([FromBody] ProductRequest request, int Id)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new UpdateProductCommand(request, Id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> DeleteProduct(int Id)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new DeleteProductCommand(Id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("filter/{id}")]
        public async Task<ApiResponse<List<ProductResponse>>> GetProductByFilter([FromQuery] int? Id)
        {
            var operation = new GetProductByFilterQuery(Id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
