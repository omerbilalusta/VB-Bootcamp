using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_DTO;

namespace Vb_Operation.Validation
{
    public class ProductValidator : AbstractValidator<ProductRequest>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name can not be empty").MaximumLength(50).MinimumLength(3);
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description can not be empty").MaximumLength(150).MinimumLength(20);
            RuleFor(x => x.Type).NotEmpty().WithMessage("Type can not be empty").MaximumLength(20);
            RuleFor(x => x.Price).NotEmpty().WithMessage("Price can not be empty").GreaterThan(0);
            RuleFor(x => x.StockQuantity).NotEmpty().WithMessage("StockQuantity can not be empty").GreaterThan(0);
            RuleFor(x => x.TaxRate).NotEmpty().WithMessage("TaxRate can not be empty").GreaterThan(0).LessThan(1);
        }
    }
}
