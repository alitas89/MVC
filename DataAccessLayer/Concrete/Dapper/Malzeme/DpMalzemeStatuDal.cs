using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace DataAccessLayer.Concrete.Dapper.Malzeme
{
    public class DpMalzemeStatuDal : DpEntityRepositoryBase<MalzemeStatu>, IMalzemeStatuDal
    {
        public List<MalzemeStatu> GetList()
        {
            return GetListQuery("select * from MalzemeStatu where Silindi=0", new { });
        }

        public MalzemeStatu Get(int Id)
        {
            return GetQuery("select * from MalzemeStatu where MalzemeStatuID= @Id and Silindi=0", new { Id });
        }

        public int Add(MalzemeStatu malzemestatu)
        {
            return AddQuery("insert into MalzemeStatu(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", malzemestatu);
        }

        public int Update(MalzemeStatu malzemestatu)
        {
            return UpdateQuery("update MalzemeStatu set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where MalzemeStatuID=@MalzemeStatuID", malzemestatu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from MalzemeStatu where MalzemeStatuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update MalzemeStatu set Silindi = 1 where MalzemeStatuID=@Id", new { Id });
        }

        public List<MalzemeStatu> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM MalzemeStatu where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM MalzemeStatu where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}
