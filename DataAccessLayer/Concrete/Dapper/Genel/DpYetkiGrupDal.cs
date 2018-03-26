using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace DataAccessLayer.Concrete.Dapper.Genel
{
    public class DpYetkiGrupDal : DpEntityRepositoryBase<YetkiGrup>, IYetkiGrupDal
    {
        public List<YetkiGrup> GetList()
        {
            return GetListQuery("select * from YetkiGrup where Silindi=0", new { });
        }

        public YetkiGrup Get(int Id)
        {
            return GetQuery("select * from YetkiGrup where YetkiGrupID= @Id and Silindi=0", new {Id});
        }

        public int Add(YetkiGrup yetkigrup)
        {
            return AddQuery("insert into YetkiGrup(Kod,Ad,Silindi) values (@Kod,@Ad,@Silindi)", yetkigrup);
        }

        public int Update(YetkiGrup yetkigrup)
        {
            return UpdateQuery("update YetkiGrup set Kod=@Kod,Ad=@Ad,Silindi=@Silindi where YetkiGrupID=@YetkiGrupID",
                yetkigrup);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from YetkiGrup where YetkiGrupID=@Id ", new {Id});
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update YetkiGrup set Silindi = 1 where YetkiGrupID=@Id", new {Id});
        }

        public List<YetkiGrup> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM YetkiGrup where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new {pagingParams.filter, pagingParams.offset, pagingParams.limit});
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_YetkiGrup where Silindi = 0 {filterQuery}",
                               new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }
    }
}