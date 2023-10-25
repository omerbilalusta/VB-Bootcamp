using AutoMapper;
using LinqKit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_Base.Response;
using Vb_Data.Domain;
using Vb_Data.UnitOfWork;
using Vb_DTO;
using Vb_Operation.Cqrs;

namespace Vb_Operation.Query
{
    public class ProductQueryHandler :
        IRequestHandler<GetAllProductQuery, ApiResponse<List<ProductResponse>>>,
        IRequestHandler<GetProductByIdQuery, ApiResponse<ProductResponse>>,
        IRequestHandler<GetProductByFilterQuery, ApiResponse<List<ProductResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<ProductResponse>>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var list = unitOfWork.ProductRepository.GetAllAsync(cancellationToken);

            var mappedList = mapper.Map<List<ProductResponse>>(list);
            return new ApiResponse<List<ProductResponse>>(mappedList);
        }

        public async Task<ApiResponse<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = unitOfWork.ProductRepository.GetByIdAsync(request.Id, cancellationToken);

            if (entity == null)
                return new ApiResponse<ProductResponse>("Product not found");

            var mapped = mapper.Map<ProductResponse>(entity);
            return new ApiResponse<ProductResponse>(mapped);
        }

        public async Task<ApiResponse<List<ProductResponse>>> Handle(GetProductByFilterQuery request, CancellationToken cancellationToken)
        {
            var predictate = PredicateBuilder.New<Product>(true);
            if (request.Id > 0)
                predictate.And(x => x.Id == request.Id);

            var list = unitOfWork.ProductRepository.Where(predictate).ToList();
            var mapped = mapper.Map<List<ProductResponse>>(list);
            return new ApiResponse<List<ProductResponse>>(mapped);
        }
    }
}
