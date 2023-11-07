using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb_Base.Response;
using Vb_Data.Domain;
using Vb_Data.UnitOfWork;
using Vb_DTO;
using Vb_Operation.Cqrs;

namespace Vb_Operation.Command
{
    public class ProductCommandHandler :
        IRequestHandler<CreateProductCommand, ApiResponse<ProductResponse>>,
        IRequestHandler<UpdateProductCommand, ApiResponse>,
        IRequestHandler<UpdateProductStockCommand, ApiResponse>,
        IRequestHandler<DeleteProductCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var mapped = mapper.Map<Product>(request.model);
            mapped.CompanyId = request.userId;
            var entity = await unitOfWork.ProductRepository.CreateAsync(mapped, request.userId, cancellationToken);
            unitOfWork.CommitAsync(cancellationToken);

            var response = mapper.Map<ProductResponse>(entity);
            return new ApiResponse<ProductResponse>(response);

        }

        public async Task<ApiResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.ProductRepository.GetAsQueryable().FirstOrDefaultAsync(x => x.Id == request.Id && x.CompanyId == request.userId, cancellationToken);
            if (entity == null)
                return new ApiResponse("Product not found");

            entity.Name = request.model.Name;
            entity.Description = request.model.Description;
            entity.Type = request.model.Type;
            entity.StockQuantity = request.model.StockQuantity;
            entity.Price = request.model.Price;
            entity.TaxRate = request.model.TaxRate;
            entity.UpdateDate = DateTime.Now;
            entity.UpdateUserId = request.userId;

            unitOfWork.ProductRepository.Update(entity, request.userId);
            unitOfWork.CommitAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = unitOfWork.ProductRepository.GetAsQueryable().FirstOrDefault(x => x.Id == request.Id);
            if (entity == null)
                return new ApiResponse("Product not found");

            unitOfWork.ProductRepository.Delete(entity, request.userId);
            unitOfWork.CommitAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(UpdateProductStockCommand request, CancellationToken cancellationToken)
        {
            var entity = unitOfWork.ProductRepository.GetAsQueryable().FirstOrDefault(x => x.Id == request.Id);
            if (entity == null)
                return new ApiResponse("Product not found");
            
            entity.StockQuantity = request.model.StockQuantity;
            entity.UpdateDate = DateTime.Now;
            entity.UpdateUserId = request.userId;

            unitOfWork.ProductRepository.Update(entity, request.userId);
            unitOfWork.CommitAsync(cancellationToken);
            return new ApiResponse();
        }
    }
}
