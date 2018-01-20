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

            Bind<ICompanyService>().To<CompanyManager>();
            Bind<ICompanyDal>().To<DpCompanyDal>();

            Bind<IProductService>().To<ProductManager>();
            Bind<IProductDal>().To<DpProductDal>();
            //Multi2
            Bind<IProductCategoryDal>().To<DpProductCategoryDal>();
            //Multi3
            Bind<IProductCategoryCompanyDal>().To<DpProductCategoryCompanyDal>();



            //*****Asıl tablolar

            //#VarlıkModul
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

            //#BakimModul
            Bind<IIsTipiService>().To<IsTipiManager>().InSingletonScope();
            Bind<IIsTipiDal>().To<DpIsTipiDal>();

            Bind<IBakimArizaKoduService>().To<BakimArizaKoduManager>().InSingletonScope();
            Bind<IBakimArizaKoduDal>().To<DpBakimArizaKoduDal>();

            Bind<IArizaCozumuService>().To<ArizaCozumuManager>().InSingletonScope();
            Bind<IArizaCozumuDal>().To<DpArizaCozumuDal>();

            Bind<IArizaNedeniGrubuService>().To<ArizaNedeniGrubuManager>().InSingletonScope();
            Bind<IArizaNedeniGrubuDal>().To<DpArizaNedeniGrubuDal>();

            Bind<IBakimOncelikService>().To<BakimOncelikManager>().InSingletonScope();
            Bind<IBakimOncelikDal>().To<DpBakimOncelikDal>();

            Bind<IUretimTipiService>().To<UretimTipiManager>().InSingletonScope();
            Bind<IUretimTipiDal>().To<DpUretimTipiDal>();

            Bind<IBakimEkibiService>().To<BakimEkibiManager>().InSingletonScope();
            Bind<IBakimEkibiDal>().To<DpBakimEkibiDal>();

            Bind<IBeklemeIptalNedeniService>().To<BeklemeIptalNedeniManager>().InSingletonScope();
            Bind<IBeklemeIptalNedeniDal>().To<DpBeklemeIptalNedeniDal>();

            Bind<IGecikmeNedeniService>().To<GecikmeNedeniManager>().InSingletonScope();
            Bind<IGecikmeNedeniDal>().To<DpGecikmeNedeniDal>();

            Bind<IBilgilendirmeTuruService>().To<BilgilendirmeTuruManager>().InSingletonScope();
            Bind<IBilgilendirmeTuruDal>().To<DpBilgilendirmeTuruDal>();

            Bind<IBilgilendirmeGrubuService>().To<BilgilendirmeGrubuManager>().InSingletonScope();
            Bind<IBilgilendirmeGrubuDal>().To<DpBilgilendirmeGrubuDal>();

            Bind<IGonderimFormatiService>().To<GonderimFormatiManager>().InSingletonScope();
            Bind<IGonderimFormatiDal>().To<DpGonderimFormatiDal>();

            Bind<IParaBirimService>().To<ParaBirimManager>().InSingletonScope();
            Bind<IParaBirimDal>().To<DpParaBirimDal>();

            Bind<IHizmetService>().To<HizmetManager>().InSingletonScope();
            Bind<IHizmetDal>().To<DpHizmetDal>();

            Bind<IRiskTipiService>().To<RiskTipiManager>().InSingletonScope();
            Bind<IRiskTipiDal>().To<DpRiskTipiDal>();

            Bind<IBakimRiskiService>().To<BakimRiskiManager>().InSingletonScope();
            Bind<IBakimRiskiDal>().To<DpBakimRiskiDal>();

            Bind<IOncelikService>().To<OncelikManager>().InSingletonScope();
            Bind<IOncelikDal>().To<DpOncelikDal>();

            Bind<IStatuTipiService>().To<StatuTipiManager>().InSingletonScope();
            Bind<IStatuTipiDal>().To<DpStatuTipiDal>();

            Bind<IStatuService>().To<StatuManager>().InSingletonScope();
            Bind<IStatuDal>().To<DpStatuDal>();

            Bind<IEtkiYeriService>().To<EtkiYeriManager>().InSingletonScope();
            Bind<IEtkiYeriDal>().To<DpEtkiYeriDal>();

            Bind<IArizaNedeniService>().To<ArizaNedeniManager>().InSingletonScope();
            Bind<IArizaNedeniDal>().To<DpArizaNedeniDal>();

            Bind<IBirimService>().To<BirimManager>().InSingletonScope();
            Bind<IBirimDal>().To<DpBirimDal>();
        }
    }
}
