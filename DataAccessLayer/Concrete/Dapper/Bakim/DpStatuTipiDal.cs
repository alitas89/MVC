using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpStatuTipiDal : DpEntityRepositoryBase<StatuTipi>, IStatuTipiDal
    {
        public List<StatuTipi> GetList()
        {
            return GetListQuery("select * from StatuTipi where Silindi=0", new { });
        }

        public StatuTipi Get(int Id)
        {
            return GetQuery("select * from StatuTipi where StatuTipiID= @Id and Silindi=0", new { Id });
        }

        public int Add(StatuTipi statutipi)
        {
            return AddQuery("insert into StatuTipi(StatuTipiAd) values (@StatuTipiAd)", statutipi);
        }

        public int Update(StatuTipi statutipi)
        {
            return UpdateQuery("update StatuTipi set StatuTipiAd=@StatuTipiAd where StatuTipiID=@StatuTipiID", statutipi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from StatuTipi where StatuTipiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update StatuTipi set Silindi = 1 where StatuTipiID=@Id", new { Id });
        }
        public List<StatuTipi> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM StatuTipi where Silindi=0 {filterQuery} {orderQuery}
        OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM StatuTipi where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}