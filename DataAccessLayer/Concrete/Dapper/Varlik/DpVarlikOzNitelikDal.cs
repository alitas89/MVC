using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpVarlikOzNitelikDal : DpEntityRepositoryBase<VarlikOzNitelik>, IVarlikOzNitelikDal
    {
        public List<VarlikOzNitelik> GetList()
        {
            return GetListQuery("select * from VarlikOzNitelik where Silindi=0", new { });
        }

        public VarlikOzNitelik Get(int Id)
        {
            return GetQuery("select * from VarlikOzNitelik where VarlikOzNitelikID= @Id and Silindi=0", new { Id });
        }

        public int Add(VarlikOzNitelik varlikoznitelik)
        {
            return AddQuery("insert into VarlikOzNitelik(VarlikID,OzNitelikID,Deger,Silindi) values (@VarlikID,@OzNitelikID,@Deger,@Silindi)", varlikoznitelik);
        }

        public int Update(VarlikOzNitelik varlikoznitelik)
        {
            return UpdateQuery("update VarlikOzNitelik set VarlikID=@VarlikID,OzNitelikID=@OzNitelikID,Deger=@Deger,Silindi=@Silindi where VarlikOzNitelikID=@VarlikOzNitelikID", varlikoznitelik);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from VarlikOzNitelik where VarlikOzNitelikID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update VarlikOzNitelik set Silindi = 1 where VarlikOzNitelikID=@Id", new { Id });
        }

        public List<VarlikOzNitelik> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM VarlikOzNitelik where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM VarlikOzNitelik where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
