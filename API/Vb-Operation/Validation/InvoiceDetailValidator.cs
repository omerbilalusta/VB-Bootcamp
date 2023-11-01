﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_DTO;

namespace Vb_Operation.Validation
{
    public class InvoiceDetailValidator : AbstractValidator<InvoiceDetailRequest>
    {
        public InvoiceDetailValidator()
        {
            RuleFor(x => x.Piece).NotEmpty().WithMessage("Piece can not be empty").GreaterThanOrEqualTo(0);
            RuleFor(x => x.TotalAmountByProduct).NotEmpty().WithMessage("TotalAmountByProduct can not be empty").GreaterThanOrEqualTo(0);
            RuleFor(x => x.InvoiceId).NotEmpty().WithMessage("InvoiceId can not be empty").GreaterThanOrEqualTo(0);
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId can not be empty").GreaterThanOrEqualTo(0);
            
        }
    }
}
