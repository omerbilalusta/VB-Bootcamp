using AutoMapper;
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
    public class PaymentCommandHandler :
        IRequestHandler<PayWithOpenAccountCommand, ApiResponse>,
        IRequestHandler<DealerPaymentCommand, ApiResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PaymentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ApiResponse> Handle(PayWithOpenAccountCommand request, CancellationToken cancellationToken)
        {
            var entityOrder =  unitOfWork.OrderRepository.GetAsQueryable("Company", "Dealer", "Invoice").FirstOrDefault(x => x.OrderNumber == request.orderNumber);
            var entityDealer = unitOfWork.DealerRepository.GetAsQueryable().FirstOrDefault(x => x.Id == request.userId);
            if (entityOrder == null)
                return new ApiResponse("Record not found");

            if(entityOrder.Amount < entityOrder.Dealer.OpenAccountLimit)
            {
                entityOrder.PaymentSuccess = true;
                entityDealer.OpenAccountLimit -= entityOrder.Amount;
                var entityInvoice = CreateInvoice(entityOrder, request.userId, cancellationToken);
                unitOfWork.CommitAsync(cancellationToken);
                return new ApiResponse();
            }

            return new ApiResponse("Open account limit not enough for payment");
        }

        public async Task<ApiResponse> Handle(DealerPaymentCommand request, CancellationToken cancellationToken)
        {
            var entityOrder = unitOfWork.OrderRepository.GetAsQueryable().FirstOrDefault(x => x.OrderNumber == request.orderNumber);
            if (entityOrder == null)
                return new ApiResponse("Order not found");

            var entityInvoice = CreateInvoice(entityOrder, request.userId, cancellationToken);

            entityOrder.InvoiceId = entityInvoice.Id;
            entityOrder.PaymentSuccess = true;                                          //Kullanıcı ödeme yaptığında Order tablosunda "paymentSucces" alanı ilgili order nesnesi için güncellenir.
            unitOfWork.OrderRepository.Update(entityOrder, request.userId);
            unitOfWork.CommitAsync(cancellationToken);
            return new ApiResponse();
        }


        private Invoice CreateInvoice(Order order, int userId, CancellationToken cancellationToken)
        {
            Invoice invoice = new Invoice()
            {
                Address = order.Address,
                OrderId = order.Id,
                Amount = order.Amount,
                PaymentMethod = order.PaymentMethod,
                InvoiceExist = true,

            };
            unitOfWork.InvoiceRepository.CreateAsync(invoice, userId, cancellationToken);
            unitOfWork.CommitAsync(cancellationToken);                                              //Bu commit'in sebebi oluşturulacak payment datası içinde InvoiceId olmalı ve Invoice nesnesi commit edildiğinde ID'si oluşur. Bu yüzden commit edildi.
            CreatePayment(invoice, userId, cancellationToken);                                     //Invoice tablosunda her invoice için bir payment datası ile ilişki olmalı.
            return invoice;                                                                         //Bu yüzden invoice data'sı oluşturulurken payment data'sıda aynı anda oluşturulur.
        }

        private void CreatePayment(Invoice invoice, int userId, CancellationToken cancellationToken)
        {
            Random random = new Random();

            Payment payment = new Payment()
            {
                PaymentMethod = invoice.PaymentMethod,
                Amount = invoice.Amount,
                ReferenceNumber = random.Next(100000, 999999),
                InvoiceId = invoice.Id
            };
            unitOfWork.PaymentRepository.CreateAsync(payment, userId, cancellationToken);
            unitOfWork.CommitAsync(cancellationToken);

            invoice.PaymentId = payment.Id;
            unitOfWork.InvoiceRepository.Update(invoice, userId);
            unitOfWork.CommitAsync(cancellationToken);
        }
    }
}
