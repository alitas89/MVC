using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Concrete.Dapper.Sistem
{
    public class DpBildirimIsTalebiSonucDal : DpEntityRepositoryBase<BildirimIsTalebiSonuc>, IBildirimIsTalebiSonucDal
    {
        public List<BildirimIsTalebiSonuc> GetList()
        {
            return GetListQuery("select * from BildirimIsTalebiSonuc where Silindi=0", new { });
        }

        public BildirimIsTalebiSonuc Get(int Id)
        {
            return GetQuery("select * from BildirimIsTalebiSonuc where BildirimIsTalebiSonucID= @Id and Silindi=0", new { Id });
        }

        public int Add(BildirimIsTalebiSonuc bildirimıstalebisonuc)
        {
            return AddQuery("insert into BildirimIsTalebiSonuc(IsEmriNoID,Silindi) values (@IsEmriNoID,@Silindi)", bildirimıstalebisonuc);
        }

        public int Update(BildirimIsTalebiSonuc bildirimıstalebisonuc)
        {
            return UpdateQuery("update BildirimIsTalebiSonuc set IsEmriNoID=@IsEmriNoID,Silindi=@Silindi where BildirimIsTalebiSonucID=@BildirimIsTalebiSonucID", bildirimıstalebisonuc);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BildirimIsTalebiSonuc where BildirimIsTalebiSonucID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BildirimIsTalebiSonuc set Silindi = 1 where BildirimIsTalebiSonucID=@Id", new { Id });
        }

        public List<BildirimIsTalebiSonuc> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM BildirimIsTalebiSonuc where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BildirimIsTalebiSonuc where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}