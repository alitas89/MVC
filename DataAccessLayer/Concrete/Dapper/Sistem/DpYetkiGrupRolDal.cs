using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Concrete.Dapper.Sistem
{
    public class DpYetkiGrupRolDal : DpEntityRepositoryBase<YetkiGrupRol>, IYetkiGrupRolDal
    {
        public List<YetkiGrupRol> GetList()
        {
            return GetListQuery("select * from YetkiGrupRol where Silindi=0", new { });
        }

        public YetkiGrupRol Get(int Id)
        {
            return GetQuery("select * from YetkiGrupRol where YetkiGrupRolID= @Id and Silindi=0", new {Id});
        }

        public int Add(YetkiGrupRol yetkigruprol)
        {
            return AddQuery(
                "insert into YetkiGrupRol(YetkiGrupID,YetkiRolKod,Silindi) values (@YetkiGrupID,@YetkiRolKod,@Silindi)",
                yetkigruprol);
        }

        public int Update(YetkiGrupRol yetkigruprol)
        {
            return UpdateQuery(
                "update YetkiGrupRol set YetkiGrupID=@YetkiGrupID,YetkiRolKod=@YetkiRolKod,Silindi=@Silindi where YetkiGrupRolID=@YetkiGrupRolID",
                yetkigruprol);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from YetkiGrupRol where YetkiGrupRolID=@Id ", new {Id});
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update YetkiGrupRol set Silindi = 1 where YetkiGrupRolID=@Id", new {Id});
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
            //columns ayrımı yapılır 
            string columnsQuery = "*";
            if (pagingParams.columns.Length != 0)
            {
                columnsQuery = pagingParams.columns;
            }

            return GetListQuery($@"SELECT * FROM YetkiGrupRol where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new {pagingParams.filter, pagingParams.offset, pagingParams.limit});
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount =
                GetScalarQuery($@"SELECT COUNT(*) FROM YetkiGrupRol where Silindi = 0 {filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public List<YetkiGrupRol> GetListByGrupId(int YetkiGrupID)
        {
            return GetListQuery("select * from YetkiGrupRol where YetkiGrupID= @YetkiGrupID and Silindi=0",
                new {YetkiGrupID});
        }

        public List<YetkiGrupRol> GetYetkiRolByYetkiGrupID(int YetkiGrupID)
        {
            return GetListQuery("select * from YetkiGrupRol where Silindi=0 and YetkiGrupID=@YetkiGrupID",
                new {YetkiGrupID});
        }

        public int DeleteSoftByYetkiGrupID(int YetkiGrupID)
        {
            return UpdateQuery("update YetkiGrupRol set Silindi = 1 where YetkiGrupID=@YetkiGrupID", new {YetkiGrupID});
        }
    }
}