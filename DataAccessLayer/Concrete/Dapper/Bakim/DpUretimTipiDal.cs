using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
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

            return GetListQuery($@"SELECT * FROM UretimTipi where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string where = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                where = $" where {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM UretimTipi {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}
