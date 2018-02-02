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
    public class IsletmeValidator : AbstractValidator<Isletme>
    {
        public IsletmeValidator()
        {
            RuleFor(p => p.Kod).NotEmpty().WithMessage("İşletme kodu boş bırakılamaz!");
            RuleFor(p => p.Ad).NotEmpty().WithMessage("İşletme tanımı boş bırakılamaz!");
        }
    }
}
