using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_Base.Response;
using Vb_Data.Context;
using Vb_Data.Domain;
using Vb_Data.UnitOfWork;
using Vb_DTO;
using Vb_Operation.Cqrs;

namespace Vb_Operation.Query
{
    public class InvoiceQueryHandler :
        IRequestHandler<GetAllInvoicesQuery, ApiResponse<List<InvoiceResponse>>>,
        IRequestHandler<GetInvoicesByCompanyDealerQuery, ApiResponse<List<InvoiceResponse>>>,
        IRequestHandler<GetInvoiceByIdQuery, ApiResponse<InvoiceResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public InvoiceQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<InvoiceResponse>>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var list = unitOfWork.InvoiceRepository.GetAsQueryable("Order", "Order.Company", "Order.Company.Products", "Order.Dealer", "Payment").Where(x => x.InvoiceExist == true).ToList();
            var mappedList = mapper.Map<List<InvoiceResponse>>(list);
            return new ApiResponse<List<InvoiceResponse>>(mappedList);
        }

        public async Task<ApiResponse<InvoiceResponse>> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.InvoiceRepository.GetByIdAsync(request.Id, cancellationToken, "Order", "Order.Company", "Order.Company.Products", "Order.Dealer", "Payment");
            if (entity is null)
            {
                return new ApiResponse<InvoiceResponse>("Record not found");
            }
            var mapped = mapper.Map<InvoiceResponse>(entity);
            return new ApiResponse<InvoiceResponse>(mapped);
        }

        public async Task<ApiResponse<List<InvoiceResponse>>> Handle(GetInvoicesByCompanyDealerQuery request, CancellationToken cancellationToken)
        {
            var list = await unitOfWork.InvoiceRepository.GetAsQueryable("Order", "Order.Company" , "Order.Company.Products", "Order.Dealer", "Payment").Where(x => x.InvoiceExist == true && (x.Order.CompanyId == request.userId || x.Order.DealerId == request.userId)).ToListAsync(cancellationToken);
            var mappedList = mapper.Map<List<InvoiceResponse>>(list);
            return new ApiResponse<List<InvoiceResponse>>(mappedList);
        }
    }
}
