using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    class ConsumptionPlaceValidator : AbstractValidator<ConsumptionPlace>
    {
        public ConsumptionPlaceValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Sarf yeri adı boş bırakılamaz!");
            RuleFor(p => p.Email).EmailAddress().NotEmpty().WithMessage("Email adresini uygun formatta giriniz");
        }
    }
}
