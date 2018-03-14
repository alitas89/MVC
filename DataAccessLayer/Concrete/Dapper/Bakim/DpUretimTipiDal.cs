using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpUretimTipiDal : DpEntityRepositoryBase<UretimTipi>, IUretimTipiDal
    {
        public List<UretimTipi> GetList()
        {
            return GetListQuery("select * from UretimTipi where Silindi=0", new { });
        }

        public UretimTipi Get(int Id)
        {
            return GetQuery("select * from UretimTipi where UretimTipiID= @Id and Silindi=0", new { Id });
        }

        public int Add(UretimTipi uretimtipi)
        {
            return AddQuery("insert into UretimTipi(UretimTipiAd) values (@UretimTipiAd)", uretimtipi);
        }

        public int Update(UretimTipi uretimtipi)
        {
            return UpdateQuery("update UretimTipi set UretimTipiAd=@UretimTipiAd where UretimTipiID=@UretimTipiID", uretimtipi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from UretimTipi where UretimTipiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update UretimTipi set Silindi = 1 where UretimTipiID=@Id", new { Id });
        }

        public List<UretimTipi> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM UretimTipi where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM UretimTipi where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}
