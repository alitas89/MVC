using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpBakimArizaKoduDal : DpEntityRepositoryBase<BakimArizaKodu>, IBakimArizaKoduDal
    {
        public List<BakimArizaKodu> GetList()
        {
            return GetListQuery("select * from BakimArizaKodu where Silindi=0", new { });
        }

        public BakimArizaKodu Get(int Id)
        {
            return GetQuery("select * from BakimArizaKodu where BakimArizaKoduID= @Id and Silindi=0", new { Id });
        }

        public int Add(BakimArizaKodu bakimarizakodu)
        {
            return AddQuery("insert BakimArizaKodu(Kod,GenelKod,Ad,IsTipiID,BakimOnceligiID,TalimatKoduID,RiskKoduID,BakimPeriyodu,BirimID,BakimSuresi,BakimPuanı,Etiket,SurecPerformansinaDahil,Aciklama,UretimTipiID,Silindi) values (@Kod,@GenelKod,@Ad,@IsTipiID,@BakimOnceligiID,@TalimatKoduID,@RiskKoduID,@BakimPeriyodu,@BirimID,@BakimSuresi,@BakimPuanı,@Etiket,@SurecPerformansinaDahil,@Aciklama,@UretimTipiID,@Silindi)", bakimarizakodu);
        }

        public int Update(BakimArizaKodu bakimarizakodu)
        {
            return UpdateQuery("update BakimArizaKodu set Kod=@Kod,GenelKod=@GenelKod,Ad=@Ad,IsTipiID=@IsTipiID,BakimOnceligiID=@BakimOnceligiID,TalimatKoduID=@TalimatKoduID,RiskKoduID=@RiskKoduID,BakimPeriyodu=@BakimPeriyodu,BirimID=@BirimID,BakimSuresi=@BakimSuresi,BakimPuanı=@BakimPuanı,Etiket=@Etiket,SurecPerformansinaDahil=@SurecPerformansinaDahil,Aciklama=@Aciklama,UretimTipiID=@UretimTipiID,Silindi=@Silindi where BakimArizaKoduID=@BakimArizaKoduID", bakimarizakodu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BakimArizaKodu where BakimArizaKoduID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BakimArizaKodu set Silindi = 1 where BakimArizaKoduID=@Id", new { Id });
        }
    }
}