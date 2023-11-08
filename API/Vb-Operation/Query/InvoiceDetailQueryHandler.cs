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
    public class OrderDetailQueryHandler :
        IRequestHandler<GetAllOrderDetailQuery, ApiResponse<List<OrderDetailResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderDetailQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<ApiResponse<List<OrderDetailResponse>>> Handle(GetAllOrderDetailQuery request, CancellationToken cancellationToken)
        {
            var list = unitOfWork.OrderDetailRepository.GetAsQueryable("Product","Order").ToList();
            var mappedList = mapper.Map<List<OrderDetailResponse>>(list);
            return new ApiResponse<List<OrderDetailResponse>>(mappedList);
        }
    }
}
