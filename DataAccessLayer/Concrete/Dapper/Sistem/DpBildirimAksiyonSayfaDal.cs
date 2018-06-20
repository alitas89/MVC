using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Concrete.Dapper.Sistem
{
    public class DpBildirimAksiyonSayfaDal : DpEntityRepositoryBase<BildirimAksiyonSayfa>, IBildirimAksiyonSayfaDal
    {
        public List<BildirimAksiyonSayfa> GetList()
        {
            return GetListQuery("select * from BildirimAksiyonSayfa where Silindi=0", new { });
        }

        public BildirimAksiyonSayfa Get(int Id)
        {
            return GetQuery("select * from BildirimAksiyonSayfa where BildirimAksiyonSayfaID= @Id and Silindi=0", new { Id });
        }

        public int Add(BildirimAksiyonSayfa bildirimaksiyonsayfa)
        {
            return AddQuery("insert into BildirimAksiyonSayfa(Ad,Url,Silindi) values (@Ad,@Url,@Silindi)", bildirimaksiyonsayfa);
        }

        public int Update(BildirimAksiyonSayfa bildirimaksiyonsayfa)
        {
            return UpdateQuery("update BildirimAksiyonSayfa set Ad=@Ad,Url=@Url,Silindi=@Silindi where BildirimAksiyonSayfaID=@BildirimAksiyonSayfaID", bildirimaksiyonsayfa);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BildirimAksiyonSayfa where BildirimAksiyonSayfaID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BildirimAksiyonSayfa set Silindi = 1 where BildirimAksiyonSayfaID=@Id", new { Id });
        }

        public List<BildirimAksiyonSayfa> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM BildirimAksiyonSayfa where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BildirimAksiyonSayfa where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}