using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_Base.Response;
using Vb_DTO;

namespace Vb_Operation.Cqrs
{
    public record GetAllInvoicesQuery() : IRequest<ApiResponse<List<InvoiceResponse>>>;
    public record GetInvoicesByCompanyDealerQuery(int userId) : IRequest<ApiResponse<List<InvoiceResponse>>>;
    public record GetInvoiceByIdQuery(int Id) : IRequest<ApiResponse<InvoiceResponse>>;
}
