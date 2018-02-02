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
    class DurusNedeniValidator : AbstractValidator<DurusNedeni>
    {
        public DurusNedeniValidator()
        {
            RuleFor(p => p.Kod).NotEmpty().WithMessage("Duruş nedeni kodu boş bırakılamaz!");
            RuleFor(p => p.Ad).NotEmpty().WithMessage("Duruş nedeni tanımı boş bırakılamaz!");
        }
    }
}
