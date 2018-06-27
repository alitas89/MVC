using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Concrete.Dapper.Sistem
{
    public class DpPeriyodikBakimBildirimOkunduDal : DpEntityRepositoryBase<PeriyodikBakimBildirimOkundu>, IPeriyodikBakimBildirimOkunduDal
    {
        public List<PeriyodikBakimBildirimOkundu> GetList()
        {
            return GetListQuery("select * from PeriyodikBakimBildirimOkundu where Silindi=0", new { });
        }

        public PeriyodikBakimBildirimOkundu Get(int Id)
        {
            return GetQuery("select * from PeriyodikBakimBildirimOkundu where PeriyodikBakimBildirimOkunduID= @Id and Silindi=0", new { Id });
        }

        public int Add(PeriyodikBakimBildirimOkundu periyodikbakimbildirimokundu)
        {
            return AddQuery("insert into PeriyodikBakimBildirimOkundu(BildirimID,KullaniciID,OkunmaTarih,Silindi) values (@BildirimID,@KullaniciID,@OkunmaTarih,@Silindi)", periyodikbakimbildirimokundu);
        }

        public int Update(PeriyodikBakimBildirimOkundu periyodikbakimbildirimokundu)
        {
            return UpdateQuery("update PeriyodikBakimBildirimOkundu set BildirimID=@BildirimID,KullaniciID=@KullaniciID,OkunmaTarih=@OkunmaTarih,Silindi=@Silindi where PeriyodikBakimBildirimOkunduID=@PeriyodikBakimBildirimOkunduID", periyodikbakimbildirimokundu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from PeriyodikBakimBildirimOkundu where PeriyodikBakimBildirimOkunduID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update PeriyodikBakimBildirimOkundu set Silindi = 1 where PeriyodikBakimBildirimOkunduID=@Id", new { Id });
        }

        public List<PeriyodikBakimBildirimOkundu> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM PeriyodikBakimBildirimOkundu where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM PeriyodikBakimBildirimOkundu where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}