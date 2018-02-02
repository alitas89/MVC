using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpOncelikDal : DpEntityRepositoryBase<Oncelik>, IOncelikDal
    {
        public List<Oncelik> GetList()
        {
            return GetListQuery("select * from Oncelik where Silindi=0", new { });
        }

        public Oncelik Get(int Id)
        {
            return GetQuery("select * from Oncelik where OncelikID= @Id and Silindi=0", new { Id });
        }

        public int Add(Oncelik oncelik)
        {
            return AddQuery("insert into Oncelik(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", oncelik);
        }

        public int Update(Oncelik oncelik)
        {
            return UpdateQuery("update Oncelik set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where OncelikID=@OncelikID", oncelik);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Oncelik where OncelikID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Oncelik set Silindi = 1 where OncelikID=@Id", new { Id });
        }
        public List<Oncelik> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Oncelik where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Oncelik {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}