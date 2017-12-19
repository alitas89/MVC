using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Dapper;
using Ninject.Modules;

namespace BusinessLayer.DependencyResolvers.Ninject
{
    public class BusinessModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IVerifyService>().To<VerifyManager>().InSingletonScope();
            Bind<IVerifyDal>().To<DpVerifyDal>();

            Bind<IKullaniciService>().To<KullaniciManager>().InSingletonScope();
            Bind<IKullaniciDal>().To<DpKullaniciDal>();

            Bind<IRolService>().To<RolManager>().InSingletonScope();
            Bind<IRolDal>().To<DpRolDal>();

            Bind<IConsumptionPlaceService>().To<ConsumptionPlaceManager>().InSingletonScope();
            Bind<IConsumptionPlaceDal>().To<DpConsumptionPlaceDal>();

            Bind<ITestService>().To<TestManager>().InSingletonScope();
            Bind<ITestDal>().To<DpTestDal>();

            Bind<IProductService>().To<ProductManager>();
            Bind<IProductDal>().To<DpProductDal>();
            //Multi2
            Bind<IProductCategoryDal>().To<DpProductCategoryDal>();
            //Multi3
            Bind<IProductCategoryCompanyDal>().To<DpProductCategoryCompanyDal>();
        }
    }
}
