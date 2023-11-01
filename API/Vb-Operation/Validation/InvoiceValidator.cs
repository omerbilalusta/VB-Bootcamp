using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_DTO;

namespace Vb_Operation.Validation
{
    public class InvoiceValidator : AbstractValidator<InvoiceRequest>
    {
        public InvoiceValidator()
        {
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount can not be empty").GreaterThanOrEqualTo(0);
            RuleFor(x => x.PaymentMethod).NotEmpty().WithMessage("PaymentMethod can not be empty");
        }
    }
}
