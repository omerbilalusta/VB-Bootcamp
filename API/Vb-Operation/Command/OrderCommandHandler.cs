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

namespace Vb_Operation.Command
{
    public class OrderCommandHandler :
        IRequestHandler<CreateOrderCommand, ApiResponse<OrderResponse>>,
        IRequestHandler<UpdateOrderCommand, ApiResponse>,
        IRequestHandler<DeleteOrderCommand, ApiResponse>,
        IRequestHandler<CompanyApproveCommand, ApiResponse>,
        IRequestHandler<DealerPaymentCommand, ApiResponse>
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
            List<InvoiceDetail> listInvoiceDetail = new List<InvoiceDetail>();
            

            var mapped = mapper.Map<Order>(request.model);
            var entity = await unitOfWork.OrderRepository.CreateAsync(mapped, request.userId, cancellationToken);
            var dealer = unitOfWork.DealerRepository.GetAsQueryable().Where(x => x.Id == request.userId).FirstOrDefault();
            decimal total = 0;
            entity.DealerId = request.userId;

            var product = unitOfWork.ProductRepository.GetAsQueryable().Where(x => x.Id == request.model.ProductList.First().Key).FirstOrDefault(); ;
            entity.CompanyId = product.CompanyId;

            Random random = new Random();
            entity.OrderNumber = random.Next(100000, 999999);

            request.model.ProductList.ForEach(x =>
            {
                var product = unitOfWork.ProductRepository.GetAsQueryable().Where(y => y.Id == x.Key).FirstOrDefault();
                total += product.Price * x.Value;
                product.StockQuantity -= x.Value;
            });
            entity.Amount = total;
            entity.Address = dealer.Address;
            entity.InsertUserId = request.userId;
            entity.InsertDate = DateTime.Now;

            unitOfWork.CommitAsync(cancellationToken);

            Invoice invoice = new Invoice()
            {
                Address = dealer.Address,
                OrderId = entity.Id,
                Amount = total,
                PaymentMethod = request.model.PaymentMethod,
                
            };
            unitOfWork.InvoiceRepository.CreateAsync(invoice, request.userId, cancellationToken);
            unitOfWork.CommitAsync(cancellationToken);

            Payment payment = new Payment()                         //Invoice tablosunda her invoice için bir payment datası ile ilişki olmalı.
            {                                                       //Bu yüzden invoice data'sı oluşturulurken payment data'sıda aynı anda oluşturulur.
                PaymentMethod = entity.PaymentMethod,               //Kullanıcı ödeme yaptığında Order tablosunda "paymentSucces" alanı ilgili order nesnesi
                Amount = entity.Amount,                             //için güncellenir. 
                ReferenceNumber = random.Next(100000, 999999),      //Kullanıcı sipariş oluştururken aynı anda invoice, payment, invoiceDetails(sipariş edilen
                InvoiceId = invoice.Id                              //product'ların listesini tutabilmek için) nesnelerinin oluşturulması veri tabanı kurgusunun
            };                                                      //zayıf olduğunun göstergesi olabilir.
            unitOfWork.PaymentRepository.CreateAsync(payment, request.userId, cancellationToken);
            unitOfWork.CommitAsync(cancellationToken);

            var orderCreated = unitOfWork.OrderRepository.GetAsQueryable().FirstOrDefault(x => x.Id == entity.Id);
            orderCreated.InvoiceId = invoice.Id;

            var invoiceCreated = unitOfWork.InvoiceRepository.GetAsQueryable().FirstOrDefault(x => x.Id == invoice.Id);
            invoiceCreated.PaymentId = payment.Id;

            request.model.ProductList.ForEach(x =>
            {
                var product = unitOfWork.ProductRepository.GetAsQueryable().Where(y => y.Id == x.Key).FirstOrDefault();
                InvoiceDetail invoiceDetail = new InvoiceDetail()
                {
                    InvoiceId = invoice.Id,                             //Siparişi oluşturulan productları tutmak için invoiceDetail tablosu oluşturuldu.
                    ProductId = x.Key,
                    Piece = x.Value,
                    TotalAmountByProduct = product.Price * x.Value
                };
                listInvoiceDetail.Add(invoiceDetail);
            });
            unitOfWork.InvoiceDetailRepository.CreateRangeAsync(listInvoiceDetail, request.userId, cancellationToken);
            unitOfWork.CommitAsync(cancellationToken);

            var response = mapper.Map<OrderResponse>(entity);
            return new ApiResponse<OrderResponse>(response);
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
            
            
            if(request.descpription != null)
            {
                unitOfWork.OrderRepository.Delete(entity, request.userId);
                OrderReject orderReject = new OrderReject()
                {
                    Description = request.descpription,
                    OrderId = entity.Id
                };
                await unitOfWork.OrderRejectRepository.CreateAsync(orderReject, request.userId, cancellationToken);
                unitOfWork.CommitAsync(cancellationToken);
                return new ApiResponse();
            }

            entity.CompanyApprove = true;                   //Eger cilenttan bir description donerse bu demek olurki company siparisi iptal etmek istiyor. 
            unitOfWork.CommitAsync(cancellationToken);      //Buna bagli olarak bir orderReject nesnesi olusturulur ve order ile baglanir. 
            return new ApiResponse();                       //Aksi takdirde order'in CompanyApprove field'i true olarak guncellenir
        }

        public async Task<ApiResponse> Handle(DealerPaymentCommand request, CancellationToken cancellationToken)
        {
            var entity = unitOfWork.OrderRepository.GetAsQueryable().FirstOrDefault(x => x.OrderNumber == request.orderNumber && x.CompanyId == request.userId); //Red edecegi order o company'ye ait olmalı, ayrica bununda kontrolu yapiliyor.
            if (entity == null)
                return new ApiResponse("Order not found");

            entity.PaymentSuccess = true;
            unitOfWork.OrderRepository.Update(entity, request.userId);
            unitOfWork.CommitAsync(cancellationToken);
            return new ApiResponse();
        }
    }
}
