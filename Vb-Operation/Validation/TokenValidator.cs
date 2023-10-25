using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vb_DTO;

namespace Vb_Operation.Validation
{
    public class TokenValidator : AbstractValidator<TokenRequest>
    {
        public TokenValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email can not be empty").MaximumLength(50);
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password can not be empty").MaximumLength(50);
        }
    }
}
