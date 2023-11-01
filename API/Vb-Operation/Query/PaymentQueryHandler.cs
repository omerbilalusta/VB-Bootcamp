using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_Base.Response;
using Vb_Data.UnitOfWork;
using Vb_DTO;
using Vb_Operation.Cqrs;

namespace Vb_Operation.Query
{
    public class PaymentQueryHandler :
        IRequestHandler<GetAllPaymentsQuery, ApiResponse<List<PaymentResponse>>>,
        IRequestHandler<GetPaymentByCompanyDealerQuery, ApiResponse<List<PaymentResponse>>>,
        IRequestHandler<GetPaymentByIdQuery, ApiResponse<PaymentResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PaymentQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetAllPaymentsQuery request, CancellationToken cancellationToken)
        {
            var list = await unitOfWork.PaymentRepository.GetAllAsync(cancellationToken);
            var mappedList = mapper.Map<List<PaymentResponse>>(list);
            return new ApiResponse<List<PaymentResponse>>(mappedList);
        }

        public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetPaymentByCompanyDealerQuery request, CancellationToken cancellationToken)
        {
            var list = await unitOfWork.PaymentRepository.GetAsQueryable("Invoice", "Invoice.Order").Where(x => x.Invoice.Order.CompanyId == request.userId || x.Invoice.Order.DealerId == request.userId).ToListAsync(cancellationToken);
            var mappedList = mapper.Map<List<PaymentResponse>>(list);
            return new ApiResponse<List<PaymentResponse>>(mappedList);
        }

        public async Task<ApiResponse<PaymentResponse>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.PaymentRepository.GetAsQueryable("Invoice", "Invoice.Order").FirstOrDefaultAsync(x => x.Id == request.Id && x.Invoice.Order.CompanyId == request.userId);
            var mapped = mapper.Map<PaymentResponse>(entity);
            return new ApiResponse<PaymentResponse>(mapped);
        }
    }
}
