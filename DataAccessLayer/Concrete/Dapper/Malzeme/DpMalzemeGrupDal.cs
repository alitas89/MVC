using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace DataAccessLayer.Concrete.Dapper.Malzeme
{
    public class DpMalzemeGrupDal : DpEntityRepositoryBase<MalzemeGrup>, IMalzemeGrupDal
    {
        public List<MalzemeGrup> GetList()
        {
            return GetListQuery("select * from MalzemeGrup where Silindi=0", new { });
        }

        public MalzemeGrup Get(int Id)
        {
            return GetQuery("select * from MalzemeGrup where MalzemeGrupID= @Id and Silindi=0", new { Id });
        }

        public int Add(MalzemeGrup malzemegrup)
        {
            return AddQuery("insert into MalzemeGrup(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", malzemegrup);
        }

        public int Update(MalzemeGrup malzemegrup)
        {
            return UpdateQuery("update MalzemeGrup set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where MalzemeGrupID=@MalzemeGrupID", malzemegrup);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from MalzemeGrup where MalzemeGrupID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update MalzemeGrup set Silindi = 1 where MalzemeGrupID=@Id", new { Id });
        }

        public List<MalzemeGrup> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM MalzemeGrup where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM MalzemeGrup {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
