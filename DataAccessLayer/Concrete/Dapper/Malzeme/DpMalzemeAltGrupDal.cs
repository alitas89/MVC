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
    public class DpMalzemeAltGrupDal : DpEntityRepositoryBase<MalzemeAltGrup>, IMalzemeAltGrupDal
    {
        public List<MalzemeAltGrup> GetList()
        {
            return GetListQuery("select * from MalzemeAltGrup where Silindi=0", new { });
        }

        public MalzemeAltGrup Get(int Id)
        {
            return GetQuery("select * from MalzemeAltGrup where MalzemeAltGrupId= @Id and Silindi=0", new { Id });
        }

        public int Add(MalzemeAltGrup malzemealtgrup)
        {
            return AddQuery("insert into MalzemeAltGrup(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", malzemealtgrup);
        }

        public int Update(MalzemeAltGrup malzemealtgrup)
        {
            return UpdateQuery("update MalzemeAltGrup set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where MalzemeAltGrupId=@MalzemeAltGrupId", malzemealtgrup);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from MalzemeAltGrup where MalzemeAltGrupId=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update MalzemeAltGrup set Silindi = 1 where MalzemeAltGrupId=@Id", new { Id });
        }

        public List<MalzemeAltGrup> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM MalzemeAltGrup where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM MalzemeAltGrup {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}
