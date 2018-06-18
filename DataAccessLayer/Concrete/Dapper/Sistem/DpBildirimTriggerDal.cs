using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Concrete.Dapper.Sistem
{
    public class DpBildirimTriggerDal : DpEntityRepositoryBase<BildirimTrigger>, IBildirimTriggerDal
    {
        public List<BildirimTrigger> GetList()
        {
            return GetListQuery("select * from BildirimTrigger where Silindi=0", new { });
        }

        public BildirimTrigger Get(int Id)
        {
            return GetQuery("select * from BildirimTrigger where BildirimTriggerID= @Id and Silindi=0", new { Id });
        }

        public int Add(BildirimTrigger bildirimtrigger)
        {
            return AddQuery("insert into BildirimTrigger(PeriyotBirimID,PeriyotDeger,OlusturanID,KimeID,KimeTipID,Ad,Icerik,BaslangicTarih,BitisTarih,BildirimAksiyonSayfaID,BildirimAksiyonID,OlusturulmaTarih,SonDegisiklikTarih,QuartzTriggerTarih,IsTrigger,Silindi) values (@PeriyotBirimID,@PeriyotDeger,@OlusturanID,@KimeID,@KimeTipID,@Ad,@Icerik,@BaslangicTarih,@BitisTarih,@BildirimAksiyonSayfaID,@BildirimAksiyonID,@OlusturulmaTarih,@SonDegisiklikTarih,@QuartzTriggerTarih,@IsTrigger,@Silindi)", bildirimtrigger);
        }

        public int Update(BildirimTrigger bildirimtrigger)
        {
            return UpdateQuery("update BildirimTrigger set PeriyotBirimID=@PeriyotBirimID,PeriyotDeger=@PeriyotDeger,OlusturanID=@OlusturanID,KimeID=@KimeID,KimeTipID=@KimeTipID,Ad=@Ad,Icerik=@Icerik,BaslangicTarih=@BaslangicTarih,BitisTarih=@BitisTarih,BildirimAksiyonSayfaID=@BildirimAksiyonSayfaID,BildirimAksiyonID=@BildirimAksiyonID,OlusturulmaTarih=@OlusturulmaTarih,SonDegisiklikTarih=@SonDegisiklikTarih,QuartzTriggerTarih=@QuartzTriggerTarih,IsTrigger=@IsTrigger,Silindi=@Silindi where BildirimTriggerID=@BildirimTriggerID", bildirimtrigger);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BildirimTrigger where BildirimTriggerID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BildirimTrigger set Silindi = 1 where BildirimTriggerID=@Id", new { Id });
        }

        public List<BildirimTrigger> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM BildirimTrigger where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BildirimTrigger where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}