using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_DTO;

namespace Vb_Operation.Validation
{
    public class CompanyValidator : AbstractValidator<CompanyRequest>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty").MinimumLength(2).MaximumLength(50);
            RuleFor(x => x.Address).NotEmpty().WithMessage("Address can not be empty").MinimumLength(10).MaximumLength(150);
            RuleFor(x => x.TaxNumber).NotEmpty().WithMessage("TaxNumber can not be empty").GreaterThanOrEqualTo(0);
        }
    }
}
