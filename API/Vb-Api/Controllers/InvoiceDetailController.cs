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
    public class InvoiceDetailController : ControllerBase
    {
        private IMediator mediator;

        public InvoiceDetailController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        public async Task<ApiResponse<List<InvoiceDetailResponse>>> GetAllInvoiceDetails()
        {
            var operation = new GetAllInvoiceDetailQuery();
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
