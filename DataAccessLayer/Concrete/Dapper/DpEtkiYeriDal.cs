using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpEtkiYeriDal : DpEntityRepositoryBase<EtkiYeri>, IEtkiYeriDal
    {
        public List<EtkiYeri> GetList()
        {
            return GetListQuery("select * from EtkiYeri where Silindi=0", new { });
        }

        public EtkiYeri Get(int Id)
        {
            return GetQuery("select * from EtkiYeri where EtkiYeriID= @Id and Silindi=0", new { Id });
        }

        public int Add(EtkiYeri etkiyeri)
        {
            return AddQuery("insert into EtkiYeri(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", etkiyeri);
        }

        public int Update(EtkiYeri etkiyeri)
        {
            return UpdateQuery("update EtkiYeri set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where EtkiYeriID=@EtkiYeriID", etkiyeri);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from EtkiYeri where EtkiYeriID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update EtkiYeri set Silindi = 1 where EtkiYeriID=@Id", new { Id });
        }

        public List<EtkiYeri> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM EtkiYeri where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM EtkiYeri {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}