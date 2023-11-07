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
    public record CreateProductCommand(ProductRequest model, int userId) : IRequest<ApiResponse<ProductResponse>>;
    public record UpdateProductCommand(ProductRequest model, int Id, int userId) : IRequest<ApiResponse>;
    public record UpdateProductStockCommand(ProductRequest2 model, int Id, int userId) : IRequest<ApiResponse>;
    public record DeleteProductCommand(int Id, int userId) : IRequest<ApiResponse>;
    public record GetAllProductQuery(int userId, string userRole) : IRequest<ApiResponse<List<ProductResponse>>>;
    public record GetProductByIdQuery(int Id, int userId) : IRequest<ApiResponse<ProductResponse>>;
    public record GetProductByFilterQuery(int userId, int? Id) : IRequest<ApiResponse<List<ProductResponse>>>;

}
