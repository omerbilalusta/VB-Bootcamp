﻿using MediatR;
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
    public class InvoiceController : ControllerBase
    {
        private IMediator mediator;

        public InvoiceController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetAllInvoices")]
        public async Task<ApiResponse<List<InvoiceResponse>>> GetAllInvoices()
        {
            var operation = new GetAllInvoicesQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetInvoiceById")]
        public async Task<ApiResponse<InvoiceResponse>> GetInvoiceById([FromQuery] int id)
        {
            var operation = new GetInvoiceByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetInvoicesByCompanyDealer")]
        public async Task<ApiResponse<List<InvoiceResponse>>> GetInvoicesByCompany()
        {
            var userId = (User.Identity as ClaimsIdentity).FindFirst("Id").Value;
            var operation = new GetInvoicesByCompanyDealerQuery(int.Parse(userId));
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
