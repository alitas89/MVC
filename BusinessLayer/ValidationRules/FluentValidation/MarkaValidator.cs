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
    class MarkaValidator : AbstractValidator<Marka>
    {
        public MarkaValidator()
        {
            RuleFor(p => p.Kod).NotEmpty().WithMessage("Marka kodu boş bırakılamaz!");
        }
    }
}
