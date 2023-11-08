using AutoMapper;
using Azure.Core;
using LinqKit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vb_Base.Response;
using Vb_Data.Domain;
using Vb_Data.Domain.User;
using Vb_Data.UnitOfWork;
using Vb_DTO;
using Vb_Operation.Cqrs;

namespace Vb_Operation.Command
{
    public class OrderCommandHandler :
        IRequestHandler<CreateOrderCommand, ApiResponse<OrderResponse>>,
        IRequestHandler<UpdateOrderCommand, ApiResponse>, 
        IRequestHandler<UpdatePaymentMethodCommand, ApiResponse>,
        IRequestHandler<DeleteOrderCommand, ApiResponse>,
        IRequestHandler<CompanyApproveCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse<OrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Random random = new Random();
            decimal total = 0;

            var mapped = mapper.Map<Order>(request.model);
            var entity = await unitOfWork.OrderRepository.CreateAsync(mapped, request.userId, cancellationToken);

            var dealer = unitOfWork.DealerRepository.GetAsQueryable().Where(x => x.Id == request.userId).FirstOrDefault();

            var products = unitOfWork.ProductRepository.GetAsQueryable().ToList();
            request.model.ProductList.ForEach(x =>          //Her bir ürün için dealer'a atanmış kar marjı ve her ürüne atanmış vergi oranı ile ürünlerin fiyatlarını hesapladık
            {                                               //ve bu toplamı order nesnesinin Amount field'ına yerleştirdik. Ayrıca satınlan ürünleri stoktan düşmek için product nesnesinide satın alınan ürün adedini düşerek güncelledik.
                var product = products.Where(y => y.Id == x.Key).FirstOrDefault();
                if (product != null)
                {
                    total += ((product.Price * product.TaxRate * dealer.Dividend) + product.Price) * x.Value;
                    product.StockQuantity -= x.Value;
                }
            });

            var product = products.FirstOrDefault(x => x.Id == request.model.ProductList.FirstOrDefault().Key);        //cilent tarafından gelen product listesinden herhangi birini, veritabanından gelen product
                                                                                                                       //listesinden bulup o product'ın sahibi olan company'nin ID'sini order'ın companyId field'ına veriyoruz.
            entity.DealerId = request.userId;                                                                          //Buradaki amaç iş kuralı olarak belirlediğim, sipariş sahibi bir company olabilir olmasından dolayıdır.
            entity.CompanyId = product.CompanyId;                                                                      //Çünkü company'ler order'ları approve veya decline etme yetkisine sahip, approve veya decline olacak order'lar için
            entity.OrderNumber = random.Next(100000, 999999);                                                          //söz sahibi tek bir company olmasının daha uygun olacağını düşündüğümdendir.
            entity.Amount = total;
            entity.Address = dealer.Address;
            entity.InsertUserId = request.userId;
            entity.InsertDate = DateTime.Now;

            unitOfWork.CommitAsync(cancellationToken);              //Burada comit kullanılmasının nedeni order'a bağlı ürün listesini tutmak üzere  oluşturduğumuz ikinci tabloda 
                                                                    //order'ın ID'sini kullanmak içindir. Commit işlemi gerçekleşmeden nesnenin ID'si oluşmamaktadır.
            CreateOrderDetail(entity, products, dealer, request, cancellationToken);  //Order içerisindeki product'ların ayrıca bir tabloda tutulmaası gerktiği için o tablo için ayrıca bir
            unitOfWork.CommitAsync(cancellationToken);                        //fonksiyon yazarak bu işlemlerin Order Create edilirken yapılmasını sağladık.

            var response = mapper.Map<OrderResponse>(entity);
            return new ApiResponse<OrderResponse>(response);
        }

        private void CreateOrderDetail (Order entity, List<Product> products, Dealer dealer, CreateOrderCommand request, CancellationToken cancellationToken)
        {
            List<OrderDetail> listOrderDetail = new List<OrderDetail>();
            request.model.ProductList.ForEach(x =>
            {
                var product = products.Where(y => y.Id == x.Key).FirstOrDefault();
                if (product != null)
                {
                    OrderDetail orderDetail = new OrderDetail()
                    {
                        OrderId = entity.Id,                             //Siparişi oluşturulan productları tutmak için orderDetail tablosu oluşturuldu.
                        ProductId = x.Key,
                        Piece = x.Value,
                        TotalAmountByProduct = ((product.Price * product.TaxRate * dealer.Dividend) + product.Price) * x.Value
                    };
                    listOrderDetail.Add(orderDetail);
                }
            });
            unitOfWork.OrderDetailRepository.CreateRangeAsync(listOrderDetail, request.userId, cancellationToken);
        }

        public async Task<ApiResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = await unitOfWork.OrderRepository.GetByIdAsync(request.Id, cancellationToken);
            if(entity == null)
                return new ApiResponse("Order not found");
            
            entity.PaymentMethod = request.model.PaymentMethod;

            unitOfWork.OrderRepository.Update(entity, request.userId);
            unitOfWork.CommitAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var entity = unitOfWork.OrderRepository.GetAsQueryable().Where(x => x.OrderNumber == request.orderNumber).FirstOrDefault();
            if(entity == null)
                return new ApiResponse("Order not found");

            unitOfWork.OrderRepository.Delete(entity, request.userId);
            unitOfWork.CommitAsync(cancellationToken);
            return new ApiResponse();
        }

        public async Task<ApiResponse> Handle(CompanyApproveCommand request, CancellationToken cancellationToken)
        {
            var entity = unitOfWork.OrderRepository.GetAsQueryable().FirstOrDefault(x => x.OrderNumber == request.orderNumber && x.CompanyId == request.userId); //Red edecegi order o company'ye ait olmalı, ayrica bununda kontrolu yapiliyor.
            if (entity == null)
                return new ApiResponse("Order not found");
            
            
            if(request.descpription != null)                                //Eger cilenttan bir description donerse bu demek olurki company siparisi iptal etmek istiyor. 
            {                                                               //Buna bagli olarak bir orderReject nesnesi olusturulur ve order ile baglanir. 
                unitOfWork.OrderRepository.Delete(entity, request.userId);  //Order red ediliyorsa aynı anda order'da silinir.
                OrderReject orderReject = new OrderReject()
                {
                    Description = request.descpription,
                    OrderId = entity.Id
                };
                await unitOfWork.OrderRejectRepository.CreateAsync(orderReject, request.userId, cancellationToken);
                unitOfWork.CommitAsync(cancellationToken);
                return new ApiResponse();
            }

            entity.CompanyApprove = true;                   
            unitOfWork.CommitAsync(cancellationToken);                    //Description gönderilmediyse order'in CompanyApprove field'i true olarak guncellenir OrderReject nesnesi oluşturulmaz.
            return new ApiResponse();                       
        }

        public async Task<ApiResponse> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
        {
            var entityOrder = unitOfWork.OrderRepository.GetAsQueryable("Invoice").FirstOrDefault(x => x.OrderNumber == request.orderNumber && x.DealerId == request.userId);
            if (entityOrder == null)
                return new ApiResponse("Order not found");

            entityOrder.PaymentMethod = request.paymentMethod;
            unitOfWork.CommitAsync(cancellationToken);
            return new ApiResponse();
        }
    }
}
