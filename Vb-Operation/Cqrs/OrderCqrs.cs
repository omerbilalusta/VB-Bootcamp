﻿using MediatR;
using Vb_Base.Response;
using Vb_DTO;

namespace Vb_Operation.Cqrs
{
    public record CreateOrderCommand(OrderRequest model, int userId) : IRequest<ApiResponse<OrderResponse>>;
    public record UpdateOrderCommand(OrderRequest model, int Id, int userId) : IRequest<ApiResponse>;
    public record DeleteOrderCommand(int Id, int userId) : IRequest<ApiResponse>;
    public record CompanyApproveCommand(int Id, int userId, string? descpription) : IRequest<ApiResponse>;
    public record GetAllOrdersQuery() : IRequest<ApiResponse<List<OrderResponse>>>;
    public record GetOrderByIdQuery(int Id, int userId) : IRequest<ApiResponse<OrderResponse>>;
    public record GetOrderByCompanyQuery(int userId) : IRequest<ApiResponse<List<OrderResponse>>>;
    public record GetOrderByDealerQuery(int userId) : IRequest<ApiResponse<List<OrderResponse>>>;
    public record GetDeclinedOrders(int userId) : IRequest<ApiResponse<List<OrderResponse>>>;
}