using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
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
            string filterQuery = "";
            string orderQuery = "ORDER BY 1";
            if (pagingParams.filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                pagingParams.filterVal = '%' + pagingParams.filterVal + '%';
                filterQuery = $"and {pagingParams.filterCol} like @filterVal";
            }

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
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string filter = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                filter = $"and {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM StatuTipi where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}