using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpParaBirimDal : DpEntityRepositoryBase<ParaBirim>, IParaBirimDal
    {
        public List<ParaBirim> GetList()
        {
            return GetListQuery("select * from ParaBirim where Silindi=0", new { });
        }

        public ParaBirim Get(int Id)
        {
            return GetQuery("select * from ParaBirim where ParaBirimID= @Id and Silindi=0", new { Id });
        }

        public int Add(ParaBirim parabirim)
        {
            return AddQuery("insert into ParaBirim(ParaBirimAd) values (@ParaBirimAd)", parabirim);
        }

        public int Update(ParaBirim parabirim)
        {
            return UpdateQuery("update ParaBirim set ParaBirimAd=@ParaBirimAd where ParaBirimID=@ParaBirimID", parabirim);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from ParaBirim where ParaBirimID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update ParaBirim set Silindi = 1 where ParaBirimID=@Id", new { Id });
        }
        public List<ParaBirim> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM ParaBirim where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM ParaBirim where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}