using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_DTO;

namespace Vb_Operation.Validation
{
    public class OrderDetailValidator : AbstractValidator<OrderDetailRequest>
    {
        public OrderDetailValidator()
        {
            RuleFor(x => x.Piece).NotEmpty().WithMessage("Piece can not be empty").GreaterThanOrEqualTo(0);
            RuleFor(x => x.TotalAmountByProduct).NotEmpty().WithMessage("TotalAmountByProduct can not be empty").GreaterThanOrEqualTo(0);
            RuleFor(x => x.OrderId).NotEmpty().WithMessage("InvoiceId can not be empty").GreaterThanOrEqualTo(0);
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId can not be empty").GreaterThanOrEqualTo(0);
            
        }
    }
}
