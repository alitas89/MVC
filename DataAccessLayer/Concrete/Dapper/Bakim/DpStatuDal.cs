using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpStatuDal : DpEntityRepositoryBase<Statu>, IStatuDal
    {
        public List<Statu> GetList()
        {
            return GetListQuery("select * from Statu where Silindi=0", new { });
        }

        public Statu Get(int Id)
        {
            return GetQuery("select * from Statu where StatuID= @Id and Silindi=0", new { Id });
        }

        public int Add(Statu statu)
        {
            return AddQuery("insert into Statu(StatuTipiID,Kod,Ad,VarlikDurumuID,KaynakSinifi1ID,KaynakSinifi2ID,KaynakSinifi3ID,Aciklama,BeklemeIptalNedeni,TalepVarsayilani,TalepOnay,TalepRed,EmirVarsayilani,IsEmriAcik,IsEmriKapali,IsEmriIptal,EkipmanCalismiyor,PlanlanmisIsEmri,IsEmrineBaslandi,IsEmriTamamlandi,IsTeslimEdildi,SorumluDegisti,BakimErtelendi,BakimDevamEdiyor,BildirimIslemleriniYoksay,KismiSatinalmaTalebiOlusturuldu,SatinalmaTalebiOlusturuldu,SatinalmaTeklifVerildi,SatinalmaTeklifDegerlendirildi,SatinalmaSiparisVerildi,MalzemelerinSatinalmaFiyatBelirlendi,SatinalmaAmbarGirisiYapildi,EpostaGonder,SMSGonder,HerKayitAsamasindaUygula,KaydiKilitle,Silindi) values (@StatuTipiID,@Kod,@Ad,@VarlikDurumuID,@KaynakSinifi1ID,@KaynakSinifi2ID,@KaynakSinifi3ID,@Aciklama,@BeklemeIptalNedeni,@TalepVarsayilani,@TalepOnay,@TalepRed,@EmirVarsayilani,@IsEmriAcik,@IsEmriKapali,@IsEmriIptal,@EkipmanCalismiyor,@PlanlanmisIsEmri,@IsEmrineBaslandi,@IsEmriTamamlandi,@IsTeslimEdildi,@SorumluDegisti,@BakimErtelendi,@BakimDevamEdiyor,@BildirimIslemleriniYoksay,@KismiSatinalmaTalebiOlusturuldu,@SatinalmaTalebiOlusturuldu,@SatinalmaTeklifVerildi,@SatinalmaTeklifDegerlendirildi,@SatinalmaSiparisVerildi,@MalzemelerinSatinalmaFiyatBelirlendi,@SatinalmaAmbarGirisiYapildi,@EpostaGonder,@SMSGonder,@HerKayitAsamasindaUygula,@KaydiKilitle,@Silindi)", statu);
        }

        public int Update(Statu statu)
        {
            return UpdateQuery("update Statu set StatuTipiID=@StatuTipiID,Kod=@Kod,Ad=@Ad,VarlikDurumuID=@VarlikDurumuID,KaynakSinifi1ID=@KaynakSinifi1ID,KaynakSinifi2ID=@KaynakSinifi2ID,KaynakSinifi3ID=@KaynakSinifi3ID,Aciklama=@Aciklama,BeklemeIptalNedeni=@BeklemeIptalNedeni,TalepVarsayilani=@TalepVarsayilani,TalepOnay=@TalepOnay,TalepRed=@TalepRed,EmirVarsayilani=@EmirVarsayilani,IsEmriAcik=@IsEmriAcik,IsEmriKapali=@IsEmriKapali,IsEmriIptal=@IsEmriIptal,EkipmanCalismiyor=@EkipmanCalismiyor,PlanlanmisIsEmri=@PlanlanmisIsEmri,IsEmrineBaslandi=@IsEmrineBaslandi,IsEmriTamamlandi=@IsEmriTamamlandi,IsTeslimEdildi=@IsTeslimEdildi,SorumluDegisti=@SorumluDegisti,BakimErtelendi=@BakimErtelendi,BakimDevamEdiyor=@BakimDevamEdiyor,BildirimIslemleriniYoksay=@BildirimIslemleriniYoksay,KismiSatinalmaTalebiOlusturuldu=@KismiSatinalmaTalebiOlusturuldu,SatinalmaTalebiOlusturuldu=@SatinalmaTalebiOlusturuldu,SatinalmaTeklifVerildi=@SatinalmaTeklifVerildi,SatinalmaTeklifDegerlendirildi=@SatinalmaTeklifDegerlendirildi,SatinalmaSiparisVerildi=@SatinalmaSiparisVerildi,MalzemelerinSatinalmaFiyatBelirlendi=@MalzemelerinSatinalmaFiyatBelirlendi,SatinalmaAmbarGirisiYapildi=@SatinalmaAmbarGirisiYapildi,EpostaGonder=@EpostaGonder,SMSGonder=@SMSGonder,HerKayitAsamasindaUygula=@HerKayitAsamasindaUygula,KaydiKilitle=@KaydiKilitle,Silindi=@Silindi where StatuID=@StatuID", statu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Statu where StatuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Statu set Silindi = 1 where StatuID=@Id", new { Id });
        }

        public List<Statu> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM Statu where Silindi=0 {filterQuery} {orderQuery}
        OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Statu where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<StatuDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<StatuDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_StatuDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_StatuDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}