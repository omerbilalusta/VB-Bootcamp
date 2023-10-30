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
            List<InvoiceDetail> listInvoiceDetail = new List<InvoiceDetail>();
            

            var mapped = mapper.Map<Order>(request.model);
            var entity = await unitOfWork.OrderRepository.CreateAsync(mapped, request.userId, cancellationToken);

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
            entity.InsertUserId = request.userId;
            entity.InsertDate = DateTime.Now;

            unitOfWork.CommitAsync(cancellationToken);

            Invoice invoice = new Invoice()
            {
                OrderId = entity.Id,
                Amount = total,
                PaymentMethod = request.model.PaymentMethod
            };
            await unitOfWork.InvoiceRepository.CreateAsync(invoice, request.userId, cancellationToken);
            unitOfWork.CommitAsync(cancellationToken);

            request.model.ProductList.ForEach(x =>
            {
                var product = unitOfWork.ProductRepository.GetAsQueryable().Where(y => y.Id == x.Key).FirstOrDefault();
                InvoiceDetail invoiceDetail = new InvoiceDetail()
                {
                    InvoiceId = invoice.Id,
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
            var entity = unitOfWork.OrderRepository.GetAsQueryable().FirstOrDefault(x => x.Id == request.Id && x.CompanyId == request.userId); //Red edecegi order o company'ye ait olmalı, ayrica bununda kontrolu yapiliyor.
            if (entity == null)
                return new ApiResponse("Something went wrong");
            
            
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
    }
}
