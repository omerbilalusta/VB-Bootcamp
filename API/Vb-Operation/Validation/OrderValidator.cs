using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_DTO;

namespace Vb_Operation.Validation
{
    public class OrderValidator : AbstractValidator<OrderRequest>
    {
        public OrderValidator()
        {
            RuleFor(x => x.PaymentMethod).NotEmpty().WithMessage("PaymentMethod can not be empty");
            RuleFor(x => x.ProductList).NotEmpty().WithMessage("ProductList can not be empty");
        }
    }
}
