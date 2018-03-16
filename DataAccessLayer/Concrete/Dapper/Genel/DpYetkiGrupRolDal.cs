using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace DataAccessLayer.Concrete.Dapper.Genel
{
    public class DpYetkiGrupRolDal : DpEntityRepositoryBase<YetkiGrupRol>, IYetkiGrupRolDal
    {
        public List<YetkiGrupRol> GetList()
        {
            return GetListQuery("select * from YetkiGrupRol where Silindi=0", new { });
        }

        public YetkiGrupRol Get(int Id)
        {
            return GetQuery("select * from YetkiGrupRol where YetkiGrupID= @Id and Silindi=0", new { Id });
        }

        public int Add(YetkiGrupRol yetkigruprol)
        {
            return AddQuery("insert into YetkiGrupRol(YetkiGrupRolID,YetkiRolID,Silindi) values (@YetkiGrupRolID,@YetkiRolID,@Silindi)", yetkigruprol);
        }

        public int Update(YetkiGrupRol yetkigruprol)
        {
            return UpdateQuery("update YetkiGrupRol set YetkiGrupRolID=@YetkiGrupRolID,YetkiRolID=@YetkiRolID,Silindi=@Silindi where YetkiGrupID=@YetkiGrupID", yetkigruprol);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from YetkiGrupRol where YetkiGrupID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update YetkiGrupRol set Silindi = 1 where YetkiGrupID=@Id", new { Id });
        }

        public List<YetkiGrupRol> GetListPagination(PagingParams pagingParams)
        {
            string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";
          
            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            return GetListQuery($@"SELECT * FROM YetkiGrupRol where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM YetkiGrupRol {filterQuery}", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<YetkiGrupRol> GetListByGrupId(int YetkiGrupID)
        {
            return GetListQuery("select * from YetkiGrupRol where YetkiGrupID= @YetkiGrupID and Silindi=0", new { YetkiGrupID });
        }
        
    }
}