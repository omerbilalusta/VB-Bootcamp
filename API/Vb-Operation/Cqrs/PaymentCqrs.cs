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
    public record PayWithOpenAccountCommand(int orderNumber, int userId) : IRequest<ApiResponse>;
    public record DealerPaymentCommand(int orderNumber, int userId) : IRequest<ApiResponse>;
    public record GetAllPaymentsQuery() : IRequest<ApiResponse<List<PaymentResponse>>>;
    public record GetPaymentByCompanyDealerQuery(int userId) : IRequest<ApiResponse<List<PaymentResponse>>>;
    public record GetPaymentByIdQuery(int Id, int userId) : IRequest<ApiResponse<PaymentResponse>>;
}
