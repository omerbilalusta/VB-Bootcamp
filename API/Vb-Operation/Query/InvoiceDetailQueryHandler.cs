using AutoMapper;
using MediatR;
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
    public class InvoiceDetailQueryHandler :
        IRequestHandler<GetAllInvoiceDetailQuery, ApiResponse<List<InvoiceDetailResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public InvoiceDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<InvoiceDetailResponse>>> Handle(GetAllInvoiceDetailQuery request, CancellationToken cancellationToken)
        {
            var list = unitOfWork.InvoiceDetailRepository.GetAsQueryable("Product","Invoice.Order").ToList();
            var mappedList = mapper.Map<List<InvoiceDetailResponse>>(list);
            return new ApiResponse<List<InvoiceDetailResponse>>(mappedList);
        }
    }
}
