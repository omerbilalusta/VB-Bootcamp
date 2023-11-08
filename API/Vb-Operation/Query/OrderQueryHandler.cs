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
    public class OrderQueryHandler :
        IRequestHandler<GetAllOrdersQuery, ApiResponse<List<OrderResponse>>>,
        IRequestHandler<GetOrderByOrderNumberQuery, ApiResponse<OrderResponse>>,
        IRequestHandler<GetOrderByCompanyQuery, ApiResponse<List<OrderResponse>>>,
        IRequestHandler<GetOrderByDealerQuery, ApiResponse<List<OrderResponse>>>,
        IRequestHandler<GetDeclinedOrders, ApiResponse<List<OrderResponse>>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<List<OrderResponse>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken) //bu metodun varligi cok mantikli degil
        {
            var list = await unitOfWork.OrderRepository.GetAllAsync(cancellationToken, "Dealer", "Company", "Company.Products");
            var mappedList = mapper.Map<List<OrderResponse>>(list);

            return new ApiResponse<List<OrderResponse>>(mappedList);
        }

        public async Task<ApiResponse<OrderResponse>> Handle(GetOrderByOrderNumberQuery request, CancellationToken cancellationToken)  //Company'ler sadece kendi urunlerini gorebilsin diye userId'de ayrica kontrol ediliyor.
        {
            
            var entity = await unitOfWork.OrderRepository.GetAsQueryable("Dealer", "Company", "Company.Products").FirstOrDefaultAsync(x => x.OrderNumber == request.orderNumber && (x.CompanyId == request.userId || x.DealerId == request.userId), cancellationToken);
            if (entity == null)
                return new ApiResponse<OrderResponse>("Order not found");

            var mapped = mapper.Map<OrderResponse>(entity);
            return new ApiResponse<OrderResponse>(mapped);
        }

        public async Task<ApiResponse<List<OrderResponse>>> Handle(GetDeclinedOrders request, CancellationToken cancellationToken) //sadece dealer'lar icin olusturulmus, decline edilen order'ları görmeleri icin olusturulmus bir metottur.
        {
            var list = await unitOfWork.OrderRepository.GetAsQueryable("Dealer", "Company", "Company.Products").Where(x=> x.IsActive == false && x.DealerId == request.userId).ToListAsync(cancellationToken);

            var mapped = mapper.Map<List<OrderResponse>>(list);
            return new ApiResponse<List<OrderResponse>>(mapped);
        }

        public async Task<ApiResponse<List<OrderResponse>>> Handle(GetOrderByCompanyQuery request, CancellationToken cancellationToken) //sadece company'ler icin kullanilacak metot
        {
            var list = await unitOfWork.OrderRepository.GetAsQueryable("Dealer","Company","Company.Products","Invoice", "OrderDetails").Where(x => x.CompanyId == request.userId).ToListAsync(cancellationToken);

            var mapped = mapper.Map<List<OrderResponse>>(list);
            return new ApiResponse<List<OrderResponse>>(mapped);
        }

        public async Task<ApiResponse<List<OrderResponse>>> Handle(GetOrderByDealerQuery request, CancellationToken cancellationToken) //sadece dealer'lar icin kullanilacak metot
        {
            var list = await unitOfWork.OrderRepository.GetAsQueryable("Dealer", "Company", "Company.Products", "Invoice", "OrderDetails").Where(x => x.DealerId == request.userId).ToListAsync(cancellationToken);

            var mapped = mapper.Map<List<OrderResponse>>(list);
            return new ApiResponse<List<OrderResponse>>(mapped);
        }
    }
}
