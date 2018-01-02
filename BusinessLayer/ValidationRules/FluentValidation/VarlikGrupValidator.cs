using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;
using FluentValidation;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    class VarlikGrupValidator : AbstractValidator<VarlikGrup>
    {
        public VarlikGrupValidator()
        {
            RuleFor(p => p.Kod).NotEmpty().WithMessage("Varlık Grup kodu boş bırakılamaz!");
        }
    }
}
