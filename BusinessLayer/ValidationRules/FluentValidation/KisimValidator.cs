using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Varlik;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class KisimValidator : AbstractValidator<Kisim>
    {
        public KisimValidator()
        {
            RuleFor(p => p.Kod).NotEmpty().WithMessage("Kısım kodu boş bırakılamaz!");
        }
    }
}
