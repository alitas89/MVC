using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;
using Core.Utilities.Dal;
using EntityLayer.ComplexTypes.DtoModel;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpIsEmriTuruDal : DpEntityRepositoryBase<IsEmriTuru>, IIsEmriTuruDal
    {
        public List<IsEmriTuru> GetList()
        {
            return GetListQuery("select * from IsEmriTuru where Silindi=0", new { });
        }

        public IsEmriTuru Get(int Id)
        {
            return GetQuery("select * from IsEmriTuru where IsEmriTuruID= @Id and Silindi=0", new { Id });
        }

        public int Add(IsEmriTuru ısemrituru)
        {
            return AddQuery("insert into IsEmriTuru(Kod,Ad,Aciklama,TekYilSayac,TekYilBaslangicSayaci,CiftYilSayac,CiftYilBaslangicSayaci,IsEmriVarsayilani,AksiyonIsEmriVarsayilani,KaizenIsEmriVarsayilani,IsTalepVarsayilani,PeriyodikBakimVarsayilani,Servis,SokmeTakma,BagliDokumanlar,Hurdalar,SayacDegerleri,IsAdimlari,BakimNoktalari,SeyahatBilgileri,EtkilenenVarliklar,Icerik,GrupOzellikleri,OlcumDegeri,IsGunlugu,BakimRiski,ArizaKodları,NedenAnalizi,OzelKodlar,KullanilanAracGerecler,Silindi) values (@Kod,@Ad,@Aciklama,@TekYilSayac,@TekYilBaslangicSayaci,@CiftYilSayac,@CiftYilBaslangicSayaci,@IsEmriVarsayilani,@AksiyonIsEmriVarsayilani,@KaizenIsEmriVarsayilani,@IsTalepVarsayilani,@PeriyodikBakimVarsayilani,@Servis,@SokmeTakma,@BagliDokumanlar,@Hurdalar,@SayacDegerleri,@IsAdimlari,@BakimNoktalari,@SeyahatBilgileri,@EtkilenenVarliklar,@Icerik,@GrupOzellikleri,@OlcumDegeri,@IsGunlugu,@BakimRiski,@ArizaKodları,@NedenAnalizi,@OzelKodlar,@KullanilanAracGerecler,@Silindi)", ısemrituru);
        }

        public int Update(IsEmriTuru ısemrituru)
        {
            return UpdateQuery("update IsEmriTuru set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,TekYilSayac=@TekYilSayac,TekYilBaslangicSayaci=@TekYilBaslangicSayaci,CiftYilSayac=@CiftYilSayac,CiftYilBaslangicSayaci=@CiftYilBaslangicSayaci,IsEmriVarsayilani=@IsEmriVarsayilani,AksiyonIsEmriVarsayilani=@AksiyonIsEmriVarsayilani,KaizenIsEmriVarsayilani=@KaizenIsEmriVarsayilani,IsTalepVarsayilani=@IsTalepVarsayilani,PeriyodikBakimVarsayilani=@PeriyodikBakimVarsayilani,Servis=@Servis,SokmeTakma=@SokmeTakma,BagliDokumanlar=@BagliDokumanlar,Hurdalar=@Hurdalar,SayacDegerleri=@SayacDegerleri,IsAdimlari=@IsAdimlari,BakimNoktalari=@BakimNoktalari,SeyahatBilgileri=@SeyahatBilgileri,EtkilenenVarliklar=@EtkilenenVarliklar,Icerik=@Icerik,GrupOzellikleri=@GrupOzellikleri,OlcumDegeri=@OlcumDegeri,IsGunlugu=@IsGunlugu,BakimRiski=@BakimRiski,ArizaKodları=@ArizaKodları,NedenAnalizi=@NedenAnalizi,OzelKodlar=@OzelKodlar,KullanilanAracGerecler=@KullanilanAracGerecler,Silindi=@Silindi where IsEmriTuruID=@IsEmriTuruID", ısemrituru);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from IsEmriTuru where IsEmriTuruID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update IsEmriTuru set Silindi = 1 where IsEmriTuruID=@Id", new { Id });
        }

        public List<IsEmriTuru> GetListPagination(PagingParams pagingParams)
        {
              string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            //columns ayrımı yapılır
            string columnsQuery = "*";
            if (pagingParams.columns.Length != 0)
            {
                columnsQuery = pagingParams.columns;
            }

            return GetListQuery($@"SELECT {columnsQuery} FROM IsEmriTuru where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM IsEmriTuru where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
