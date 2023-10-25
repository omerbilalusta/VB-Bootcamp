using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_DTO;

namespace Vb_Operation.Validation
{
    public class PaymentValidator : AbstractValidator<PaymentRequest>
    {
        public PaymentValidator()
        {
            RuleFor(x => x.PaymentMethod).NotEmpty().WithMessage("PaymentMethod can not be empty").MaximumLength(10).MinimumLength(2);
            RuleFor(x => x.Amount).NotEmpty().WithMessage("Amount can not be empty").GreaterThanOrEqualTo(0);
            RuleFor(x => x.ReferenceNumber).NotEmpty().WithMessage("ReferenceNumber can not be empty").GreaterThanOrEqualTo(0);
        }
    }
}
