using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpGecikmeNedeniDal : DpEntityRepositoryBase<GecikmeNedeni>, IGecikmeNedeniDal
    {
        public List<GecikmeNedeni> GetList()
        {
            return GetListQuery("select * from GecikmeNedeni where Silindi=0", new { });
        }

        public GecikmeNedeni Get(int Id)
        {
            return GetQuery("select * from GecikmeNedeni where GecikmeNedeniID= @Id and Silindi=0", new { Id });
        }

        public int Add(GecikmeNedeni gecikmenedeni)
        {
            return AddQuery("insert into GecikmeNedeni(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", gecikmenedeni);
        }

        public int Update(GecikmeNedeni gecikmenedeni)
        {
            return UpdateQuery("update GecikmeNedeni set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where GecikmeNedeniID=@GecikmeNedeniID", gecikmenedeni);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from GecikmeNedeni where GecikmeNedeniID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update GecikmeNedeni set Silindi = 1 where GecikmeNedeniID=@Id", new { Id });
        }

        public List<GecikmeNedeni> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM GecikmeNedeni where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM GecikmeNedeni {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}