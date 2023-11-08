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
    public class CompanyController : ControllerBase
    {
        private IMediator mediator;

        public CompanyController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetAllCompanies")]
        public async Task<ApiResponse<List<CompanyResponse>>> GetAllCompanies()
        {
            var operation = new GetAllCompanyQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetCompanyById")]
        public async Task<ApiResponse<CompanyResponse>> GetCompanyById([FromQuery] int Id)
        {
            var operation = new GetCompanyByIdQuery(Id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
