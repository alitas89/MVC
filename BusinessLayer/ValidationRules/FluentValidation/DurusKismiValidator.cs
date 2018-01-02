using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    class DurusKismiValidator : AbstractValidator<DurusKismi>
    {
        public DurusKismiValidator()
        {
            RuleFor(p => p.Kod).NotEmpty().WithMessage("Duruş kısmı kodu boş bırakılamaz!");
            RuleFor(p => p.Ad).NotEmpty().WithMessage("Duruş kısmı tanımı boş bırakılamaz!");
        }
    }
}
