using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpBakimDurumuDal : DpEntityRepositoryBase<BakimDurumu>, IBakimDurumuDal
    {
        public List<BakimDurumu> GetList()
        {
            return GetListQuery("select * from BakimDurumu where Silindi=0", new { });
        }

        public BakimDurumu Get(int Id)
        {
            return GetQuery("select * from BakimDurumu where BakimDurumuID= @Id and Silindi=0", new { Id });
        }

        public int Add(BakimDurumu bakimdurumu)
        {
            return AddQuery("insert into BakimDurumu(Kod,Ad,Silindi) values (@Kod,@Ad,@Silindi)", bakimdurumu);
        }

        public int Update(BakimDurumu bakimdurumu)
        {
            return UpdateQuery("update BakimDurumu set Kod=@Kod,Ad=@Ad,Silindi=@Silindi where BakimDurumuID=@BakimDurumuID", bakimdurumu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BakimDurumu where BakimDurumuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BakimDurumu set Silindi = 1 where BakimDurumuID=@Id", new { Id });
        }

        public List<BakimDurumu> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM BakimDurumu where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BakimDurumu where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}