using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using BusinessLayer.Abstract.Malzeme;
using BusinessLayer.Abstract.Personel;
using BusinessLayer.Abstract.Varlik;
using BusinessLayer.Concrete;
using BusinessLayer.Concrete.Bakim;
using BusinessLayer.Concrete.Malzeme;
using BusinessLayer.Concrete.Personel;
using BusinessLayer.Concrete.Varlik;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using DataAccessLayer.Abstract.Malzeme;
using DataAccessLayer.Abstract.Personel;
using DataAccessLayer.Abstract.Varlik;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Dapper;
using DataAccessLayer.Concrete.Dapper.Bakim;
using DataAccessLayer.Concrete.Dapper.Malzeme;
using DataAccessLayer.Concrete.Dapper.Personel;
using DataAccessLayer.Concrete.Dapper.Varlik;
using Ninject.Modules;
using BusinessLayer.Abstract.Satinalma;
using BusinessLayer.Abstract.Sistem;
using DataAccessLayer.Abstract.Satinalma;
using BusinessLayer.Concrete.Satinalma;
using BusinessLayer.Concrete.Sistem;
using DataAccessLayer.Abstract.Sistem;
using DataAccessLayer.Concrete.Dapper.Satinalma;
using DataAccessLayer.Concrete.Dapper.Sistem;

namespace BusinessLayer.DependencyResolvers.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            //#SistemModul
            Bind<IVerifyService>().To<VerifyManager>().InSingletonScope();
            Bind<IVerifyDal>().To<DpVerifyDal>();

            Bind<IKullaniciService>().To<KullaniciManager>().InSingletonScope();
            Bind<IKullaniciDal>().To<DpKullaniciDal>();

            Bind<ICompanyService>().To<CompanyManager>();
            Bind<ICompanyDal>().To<DpCompanyDal>();

            Bind<IYetkiGrupKullaniciService>().To<YetkiGrupKullaniciManager>();
            Bind<IYetkiGrupKullaniciDal>().To<DpYetkiGrupKullaniciDal>();

            Bind<IYetkiGrupRolService>().To<YetkiGrupRolManager>();
            Bind<IYetkiGrupRolDal>().To<DpYetkiGrupRolDal>();

            Bind<IYetkiRolService>().To<YetkiRolManager>();
            Bind<IYetkiRolDal>().To<DpYetkiRolDal>();

            Bind<IYetkiGrupService>().To<YetkiGrupManager>();
            Bind<IYetkiGrupDal>().To<DpYetkiGrupDal>();

            Bind<IMenuService>().To<MenuManager>();
            Bind<IMenuDal>().To<DpMenuDal>();

            Bind<IIsTalebiOnayBirimService>().To<IsTalebiOnayBirimManager>().InSingletonScope();
            Bind<IIsTalebiOnayBirimDal>().To<DpIsTalebiOnayBirimDal>();

            Bind<IIsTalebiBirimService>().To<IsTalebiBirimManager>().InSingletonScope();
            Bind<IIsTalebiBirimDal>().To<DpIsTalebiBirimDal>();

            Bind<IGenelBildirimService>().To<GenelBildirimManager>().InSingletonScope();
            Bind<IGenelBildirimDal>().To<DpGenelBildirimDal>();

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

            Bind<IVarlikService>().To<VarlikManager>().InSingletonScope();
            Bind<IVarlikDal>().To<DpVarlikDal>();

            Bind<IAracServisService>().To<AracServisManager>().InSingletonScope();
            Bind<IAracServisDal>().To<DpAracServisDal>();

            Bind<IAkaryakitAlimFisService>().To<AkaryakitAlimFisManager>().InSingletonScope();
            Bind<IAkaryakitAlimFisDal>().To<DpAkaryakitAlimFisDal>();

            Bind<IZimmetTransferService>().To<ZimmetTransferManager>().InSingletonScope();
            Bind<IZimmetTransferDal>().To<DpZimmetTransferDal>();

            Bind<IZimmetTransferDetayService>().To<ZimmetTransferDetayManager>().InSingletonScope();
            Bind<IZimmetTransferDetayDal>().To<DpZimmetTransferDetayDal>();

            Bind<IVarlikTransferService>().To<VarlikTransferManager>().InSingletonScope();
            Bind<IVarlikTransferDal>().To<DpVarlikTransferDal>();

            Bind<IKaynakService>().To<KaynakManager>().InSingletonScope();
            Bind<IKaynakDal>().To<DpKaynakDal>();

            Bind<IKaynakTuruService>().To<KaynakTuruManager>().InSingletonScope();
            Bind<IKaynakTuruDal>().To<DpKaynakTuruDal>();

            Bind<IKaynakTipiService>().To<KaynakTipiManager>().InSingletonScope();
            Bind<IKaynakTipiDal>().To<DpKaynakTipiDal>();

            Bind<IKaynakDurumuService>().To<KaynakDurumuManager>().InSingletonScope();
            Bind<IKaynakDurumuDal>().To<DpKaynakDurumuDal>();

            Bind<IVarlikSablonService>().To<VarlikSablonManager>().InSingletonScope();
            Bind<IVarlikSablonDal>().To<DpVarlikSablonDal>();

            Bind<IOzNitelikService>().To<OzNitelikManager>().InSingletonScope();
            Bind<IOzNitelikDal>().To<DpOzNitelikDal>();

            Bind<IVarlikOzNitelikService>().To<VarlikOzNitelikManager>().InSingletonScope();
            Bind<IVarlikOzNitelikDal>().To<DpVarlikOzNitelikDal>();

            Bind<IYakitService>().To<YakitManager>().InSingletonScope();
            Bind<IYakitDal>().To<DpYakitDal>();

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

            Bind<IBakimEkibiUyeService>().To<BakimEkibiUyeManager>().InSingletonScope();
            Bind<IBakimEkibiUyeDal>().To<DpBakimEkibiUyeDal>();

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

            Bind<IIsEmriTuruService>().To<IsEmriTuruManager>().InSingletonScope();
            Bind<IIsEmriTuruDal>().To<DpIsEmriTuruDal>();

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

            Bind<IIsEmriService>().To<IsEmriManager>().InSingletonScope();
            Bind<IIsEmriDal>().To<DpIsEmriDal>();

            Bind<IIsTipiEmirTuruService>().To<IsTipiEmirTuruManager>().InSingletonScope();
            Bind<IIsTipiEmirTuruDal>().To<DpIsTipiEmirTuruDal>();

            Bind<IIsEmriNoService>().To<IsEmriNoManager>().InSingletonScope();
            Bind<IIsEmriNoDal>().To<DpIsEmriNoDal>();

            Bind<IBakimDurumuService>().To<BakimDurumuManager>().InSingletonScope();
            Bind<IBakimDurumuDal>().To<DpBakimDurumuDal>();

            //#MalzemeModul
            Bind<IAmbarService>().To<AmbarManager>().InSingletonScope();
            Bind<IAmbarDal>().To<DpAmbarDal>();

            Bind<IMalzemeGrupService>().To<MalzemeGrupManager>().InSingletonScope();
            Bind<IMalzemeGrupDal>().To<DpMalzemeGrupDal>();

            Bind<IMalzemeAltGrupService>().To<MalzemeAltGrupManager>().InSingletonScope();
            Bind<IMalzemeAltGrupDal>().To<DpMalzemeAltGrupDal>();

            Bind<IMalzemeSeriNoService>().To<MalzemeSeriNoManager>().InSingletonScope();
            Bind<IMalzemeSeriNoDal>().To<DpMalzemeSeriNoDal>();

            Bind<IMalzemeStatuService>().To<MalzemeStatuManager>().InSingletonScope();
            Bind<IMalzemeStatuDal>().To<DpMalzemeStatuDal>();

            Bind<IOlcuBirimService>().To<OlcuBirimManager>().InSingletonScope();
            Bind<IOlcuBirimDal>().To<DpOlcuBirimDal>();

            Bind<IMuhasebeHesapService>().To<MuhasebeHesapManager>().InSingletonScope();
            Bind<IMuhasebeHesapDal>().To<DpMuhasebeHesapDal>();

            Bind<IMalzemeService>().To<MalzemeManager>().InSingletonScope();
            Bind<IMalzemeDal>().To<DpMalzemeDal>();

            Bind<IMalzemeHareketService>().To<MalzemeHareketManager>().InSingletonScope();
            Bind<IMalzemeHareketDal>().To<DpMalzemeHareketDal>();

            Bind<IMalzemeHareketFisService>().To<MalzemeHareketFisManager>().InSingletonScope();
            Bind<IMalzemeHareketFisDal>().To<DpMalzemeHareketFisDal>();

            Bind<IMalzemeHareketDetayService>().To<MalzemeHareketDetayManager>().InSingletonScope();
            Bind<IMalzemeHareketDetayDal>().To<DpMalzemeHareketDetayDal>();

            //#PersonelModul
            Bind<IKaynakSinifiService>().To<KaynakSinifiManager>().InSingletonScope();
            Bind<IKaynakSinifiDal>().To<DpKaynakSinifiDal>();

            Bind<IVardiyaSinifiService>().To<VardiyaSinifiManager>().InSingletonScope();
            Bind<IVardiyaSinifiDal>().To<DpVardiyaSinifiDal>();

            Bind<IMesaiService>().To<MesaiManager>().InSingletonScope();
            Bind<IMesaiDal>().To<DpMesaiDal>();

            Bind<IMesaiTuruService>().To<MesaiTuruManager>().InSingletonScope();
            Bind<IMesaiTuruDal>().To<DpMesaiTuruDal>();

            Bind<IVardiyaService>().To<VardiyaManager>().InSingletonScope();
            Bind<IVardiyaDal>().To<DpVardiyaDal>();

            Bind<IKaynakPozisyonuService>().To<KaynakPozisyonuManager>().InSingletonScope();
            Bind<IKaynakPozisyonuDal>().To<DpKaynakPozisyonuDal>();

            Bind<IEgitimService>().To<EgitimManager>().InSingletonScope();
            Bind<IEgitimDal>().To<DpEgitimDal>();

            //#Satinalma
            Bind<IBelgeTuruService>().To<BelgeTuruManager>().InSingletonScope();
            Bind<IBelgeTuruDal>().To<DpBelgeTuruDal>();

            Bind<IIsSektoruService>().To<IsSektoruManager>().InSingletonScope();
            Bind<IIsSektoruDal>().To<DpIsSektoruDal>();

            Bind<IMasrafTuruService>().To<MasrafTuruManager>().InSingletonScope();
            Bind<IMasrafTuruDal>().To<DpMasrafTuruDal>();

            Bind<IOdemeSekliService>().To<OdemeSekliManager>().InSingletonScope();
            Bind<IOdemeSekliDal>().To<DpOdemeSekliDal>();

            Bind<ITeklifIstemeSablonService>().To<TeklifIstemeSablonManager>().InSingletonScope();
            Bind<ITeklifIstemeSablonDal>().To<DpTeklifIstemeSablonDal>();

            Bind<ITeminSuresiService>().To<TeminSuresiManager>().InSingletonScope();
            Bind<ITeminSuresiDal>().To<DpTeminSuresiDal>();

            Bind<ITeslimSekliService>().To<TeslimSekliManager>().InSingletonScope();
            Bind<ITeslimSekliDal>().To<DpTeslimSekliDal>();

            Bind<ITeslimYeriService>().To<TeslimYeriManager>().InSingletonScope();
            Bind<ITeslimYeriDal>().To<DpTeslimYeriDal>();

            Bind<IIsTalebiService>().To<IsTalebiManager>().InSingletonScope();
            Bind<IIsTalebiDal>().To<DpIsTalebiDal>();
        }
    }
}
