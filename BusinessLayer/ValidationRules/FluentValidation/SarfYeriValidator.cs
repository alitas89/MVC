using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    class SarfYeriValidator : AbstractValidator<SarfYeri>
    {
        public SarfYeriValidator()
        {
            RuleFor(p => p.Kod).NotEmpty().WithMessage("SarfYeri kodu boş bırakılamaz!");
            RuleFor(p => p.Kod).MaximumLength(50).WithMessage("SarfYeri Kodu 50 karakterden fazla olamaz!");
        }
    }
}
