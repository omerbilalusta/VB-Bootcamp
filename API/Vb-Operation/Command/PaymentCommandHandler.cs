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

namespace Vb_Operation.Command
{
    public class PaymentCommandHandler :
        IRequestHandler<PayWithOpenAccountCommand, ApiResponse>
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
            var entityInvoice = unitOfWork.InvoiceRepository.GetAsQueryable("Order").FirstOrDefault(x => x.Order.OrderNumber == request.orderNumber);
            if (entityOrder == null)
                return new ApiResponse("Record not found");

            if(entityOrder.Amount < entityOrder.Dealer.OpenAccountLimit)
            {
                entityOrder.PaymentSuccess = true;
                entityDealer.OpenAccountLimit -= entityOrder.Amount;
                entityInvoice.InvoiceExist = true;
                unitOfWork.CommitAsync(cancellationToken);
                return new ApiResponse();
            }

            return new ApiResponse("Open account limit not enough for payment");
        }
    }
}
