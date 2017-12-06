using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.ValidationRules.FluentValidation;
using EntityLayer.Concrete;
using FluentValidation;
using Ninject.Modules;

namespace BusinessLayer.DependencyResolvers.Ninject
{
    public class ValidationModule:NinjectModule
    {
        public override void Load()
        {
            Bind<IValidator<ConsumptionPlace>>().To<ConsumptionPlaceValidator>().InSingletonScope();
            Bind<IValidator<Product>>().To<ProductValidator>().InSingletonScope();
        }
    }
}
