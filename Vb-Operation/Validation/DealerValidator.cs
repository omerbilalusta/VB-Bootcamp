using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_DTO;

namespace Vb_Operation.Validation
{
    public class DealerValidator : AbstractValidator<DealerRequest>
    {
        public DealerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty").MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.Email).NotEmpty().WithMessage("Name can not be empty").MinimumLength(2).MaximumLength(50).EmailAddress();
            RuleFor(x => x.Password).NotEmpty().WithMessage("Name can not be empty").MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address can not be empty").MinimumLength(10).MaximumLength(150);
            RuleFor(x => x.InvoiceAddress).NotEmpty().WithMessage("InvoiceAddress can not be empty").MinimumLength(10).MaximumLength(150);
            RuleFor(x => x.Dividend).NotEmpty().WithMessage("Dividend can not be empty").GreaterThan(0).LessThan(1);
            RuleFor(x => x.OpenAccountLimit).NotEmpty().WithMessage("OpenAccountLimit can not be empty").GreaterThan(0);
        }
    }
}
