using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace DataAccessLayer.Concrete.Dapper.Genel
{
    public class DpYetkiRolDal : DpEntityRepositoryBase<YetkiRol>, IYetkiRolDal
    {
        public List<YetkiRol> GetList()
        {
            return GetListQuery("select * from YetkiRol where Silindi=0", new { });
        }

        public YetkiRol Get(int Id)
        {
            return GetQuery("select * from YetkiRol where YetkiRolID= @Id and Silindi=0", new { Id });
        }

        public int Add(YetkiRol yetkirol)
        {
            return AddQuery("insert into YetkiRol(Ad,Silindi) values (@Ad,@Silindi)", yetkirol);
        }

        public int Update(YetkiRol yetkirol)
        {
            return UpdateQuery("update YetkiRol set Ad=@Ad,Silindi=@Silindi where YetkiRolID=@YetkiRolID", yetkirol);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from YetkiRol where YetkiRolID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update YetkiRol set Silindi = 1 where YetkiRolID=@Id", new { Id });
        }

        public List<YetkiRol> GetListPagination(PagingParams pagingParams)
        {
            string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";
         
            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            return GetListQuery($@"SELECT * FROM YetkiRol where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM YetkiRol {filterQuery}", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}