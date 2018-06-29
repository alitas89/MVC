using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Concrete.Dapper.Sistem
{
    public class DpPeriyodikBakimBildirimPushedDal : DpEntityRepositoryBase<PeriyodikBakimBildirimPushed>, IPeriyodikBakimBildirimPushedDal
    {
        public List<PeriyodikBakimBildirimPushed> GetList()
        {
            return GetListQuery("select * from PeriyodikBakimBildirimPushed where Silindi=0", new { });
        }

        public PeriyodikBakimBildirimPushed Get(int Id)
        {
            return GetQuery("select * from PeriyodikBakimBildirimPushed where PeriyodikBakimBildirimPushedID= @Id and Silindi=0", new { Id });
        }

        public int Add(PeriyodikBakimBildirimPushed periyodikbakimbildirimpushed)
        {
            return AddQuery("insert into PeriyodikBakimBildirimPushed(BildirimID,KullaniciID,PushTarih,Silindi) values (@BildirimID,@KullaniciID,@PushTarih,@Silindi)", periyodikbakimbildirimpushed);
        }

        public int Update(PeriyodikBakimBildirimPushed periyodikbakimbildirimpushed)
        {
            return UpdateQuery("update PeriyodikBakimBildirimPushed set BildirimID=@BildirimID,KullaniciID=@KullaniciID,PushTarih=@PushTarih,Silindi=@Silindi where PeriyodikBakimBildirimPushedID=@PeriyodikBakimBildirimPushedID", periyodikbakimbildirimpushed);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from PeriyodikBakimBildirimPushed where PeriyodikBakimBildirimPushedID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update PeriyodikBakimBildirimPushed set Silindi = 1 where PeriyodikBakimBildirimPushedID=@Id", new { Id });
        }

        public List<PeriyodikBakimBildirimPushed> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM PeriyodikBakimBildirimPushed where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM PeriyodikBakimBildirimPushed where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}