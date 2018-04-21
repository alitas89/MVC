using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Concrete.Dapper.Sistem
{
    public class DpMenuDal : DpEntityRepositoryBase<Menu>, IMenuDal
    {
        public List<Menu> GetList()
        {
            return GetListQuery("select * from Menu where Silindi=0", new { });
        }

        public Menu Get(int Id)
        {
            return GetQuery("select * from Menu where MenuID= @Id and Silindi=0", new { Id });
        }

        public int Add(Menu menu)
        {
            return AddQuery("insert into Menu(Kod,Ad,ParentID,ModulTip,Icon,Url,Sira,Silindi) values (@Kod,@Ad,@ParentID,@ModulTip,@Icon,@Url,@Sira,@Silindi)", menu);
        }

        public int Update(Menu menu)
        {
            return UpdateQuery("update Menu set Kod=@Kod,Ad=@Ad,ParentID=@ParentID,ModulTip=@ModulTip,Icon=@Icon,Url=@Url,Sira=@Sira,Silindi=@Silindi where MenuID=@MenuID", menu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Menu where MenuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Menu set Silindi = 1 where MenuID=@Id", new { Id });
        }

        public List<Menu> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Menu where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Menu where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
