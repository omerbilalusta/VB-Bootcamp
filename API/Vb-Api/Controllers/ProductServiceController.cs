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
    public class ProductServiceController : ControllerBase
    {
        private IMediator mediator;

        public ProductServiceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("GetAllProducts")]
        [Authorize(Roles = "dealer")]
        public async Task<ApiResponse<List<ProductResponse>>> GetAllProducts()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var userRole = (User.Identity as ClaimsIdentity).FindFirst("Role").Value;
            var operation = new GetAllProductQuery(int.Parse(userId), userRole);
            var result = await mediator.Send(operation);
            return result;
        }


        [HttpGet("GetProdutsById")]
        [Authorize(Roles = "dealer")]
        public async Task<ApiResponse<ProductResponse>> GetProdutsById([FromQuery] int Id)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new GetProductByIdQuery(Id, int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }

        [Authorize(Roles = "dealer")]
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
