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
    public class ReportController : ControllerBase
    {
        private IMediator mediator;

        public ReportController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetReportByDate")]
        public async Task<ApiResponse<ReportResponse>> GetReportByDate([FromQuery] DateTime dateFrom, [FromQuery] DateTime dateTo)
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new GetReportByDateQuery(Int32.Parse(userId), dateFrom, dateTo);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
