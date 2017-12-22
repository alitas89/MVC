using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.DataAccessLayer.Abstract;
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



            //Asıl tablolar
            Bind<IDurusKismiService>().To<DurusKismiManager>().InSingletonScope();
            Bind<IDurusKismiDal>().To<DpDurusKismiDal>();

            Bind<IDurusNedeniService>().To<DurusNedeniManager>().InSingletonScope();
            Bind<IDurusNedeniDal>().To<DpDurusNedeniDal>();

            Bind<IHurdaService>().To<HurdaManager>().InSingletonScope();
            Bind<IHurdaDal>().To<DpHurdaDal>();

            Bind<IIsletmeService>().To<IsletmeManager>().InSingletonScope();
            Bind<IIsletmeDal>().To<DpIsletmeDal>();

            Bind<IKisimService>().To<KisimManager>().InSingletonScope();
            Bind<IKisimDal>().To<DpKisimDal>();

            Bind<IMarkaService>().To<MarkaManager>().InSingletonScope();
            Bind<IMarkaDal>().To<DpMarkaDal>();

            Bind<IModelService>().To<ModelManager>().InSingletonScope();
            Bind<IModelDal>().To<DpModelDal>();

            Bind<ISarfYeriService>().To<SarfYeriManager>().InSingletonScope();
            Bind<ISarfYeriDal>().To<DpSarfYeriDal>();

            Bind<IUrunService>().To<UrunManager>().InSingletonScope();
            Bind<IUrunDal>().To<DpUrunDal>();

            Bind<IVarlikDurumuService>().To<VarlikDurumuManager>().InSingletonScope();
            Bind<IVarlikDurumuDal>().To<DpVarlikDurumuDal>();

            Bind<IVarlikGrupService>().To<VarlikGrupManager>().InSingletonScope();
            Bind<IVarlikGrupDal>().To<DpVarlikGrupDal>();

            Bind<IVarlikTuruService>().To<VarlikTuruManager>().InSingletonScope();
            Bind<IVarlikTuruDal>().To<DpVarlikTuruDal>();
        }
    }
}
