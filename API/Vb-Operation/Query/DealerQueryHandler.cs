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
    public class DealerQueryHandler :
        IRequestHandler<GetAllDealerQuery, ApiResponse<List<DealerResponse>>>,
        IRequestHandler<GetDealerByIdQuery, ApiResponse<DealerResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DealerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<DealerResponse>>> Handle(GetAllDealerQuery request, CancellationToken cancellationToken)
        {
            //var list = await unitOfWork.DealerRepository.GetAllAsync(cancellationToken, "Orders", "Orders.Invoice.InvoiceDetails");
            var list = await unitOfWork.DealerRepository.GetAllAsync(cancellationToken, "Orders");
            var mappedList = mapper.Map<List<DealerResponse>>(list);
            return new ApiResponse<List<DealerResponse>>(mappedList);
        }

        public async Task<ApiResponse<DealerResponse>> Handle(GetDealerByIdQuery request, CancellationToken cancellationToken)
        {
            //var entity = await unitOfWork.DealerRepository.GetByIdAsync(request.id, cancellationToken, "Orders", "Orders.Invoice.InvoiceDetails");
            var entity = await unitOfWork.DealerRepository.GetByIdAsync(request.id, cancellationToken, "Orders");
            if (entity is null)
            {
                return new ApiResponse<DealerResponse>("Record not found");
            }
            var mapped = mapper.Map<DealerResponse>(entity);
            return new ApiResponse<DealerResponse>(mapped);
        }
    }
}
