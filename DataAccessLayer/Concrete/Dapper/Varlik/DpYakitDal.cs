using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpYakitDal : DpEntityRepositoryBase<Yakit>, IYakitDal
    {
        public List<Yakit> GetList()
        {
            return GetListQuery("select * from Yakit where Silindi=0", new { });
        }

        public Yakit Get(int Id)
        {
            return GetQuery("select * from Yakit where YakitID= @Id and Silindi=0", new { Id });
        }

        public int Add(Yakit yakit)
        {
            return AddQuery("insert into Yakit(Ad,Silindi) values (@Ad,@Silindi)", yakit);
        }

        public int Update(Yakit yakit)
        {
            return UpdateQuery("update Yakit set Ad=@Ad,Silindi=@Silindi where YakitID=@YakitID", yakit);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Yakit where YakitID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Yakit set Silindi = 1 where YakitID=@Id", new { Id });
        }

        public List<Yakit> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Yakit where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Yakit where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }
    }
}
