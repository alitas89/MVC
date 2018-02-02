using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpArizaNedeniDal : DpEntityRepositoryBase<ArizaNedeni>, IArizaNedeniDal
    {
        public List<ArizaNedeni> GetList()
        {
            return GetListQuery("select * from ArizaNedeni where Silindi=0", new { });
        }

        public ArizaNedeni Get(int Id)
        {
            return GetQuery("select * from ArizaNedeni where ArizaNedeniID= @Id and Silindi=0", new { Id });
        }

        public int Add(ArizaNedeni arizanedeni)
        {
            return AddQuery("insert into ArizaNedeni(Kod,GenelKod,Ad,UretimiDurdurur,NedenAnaliziZorunluOlmali,Aciklama,Silindi) values (@Kod,@GenelKod,@Ad,@UretimiDurdurur,@NedenAnaliziZorunluOlmali,@Aciklama,@Silindi)", arizanedeni);
        }

        public int Update(ArizaNedeni arizanedeni)
        {
            return UpdateQuery("update ArizaNedeni set Kod=@Kod,GenelKod=@GenelKod,Ad=@Ad,UretimiDurdurur=@UretimiDurdurur,NedenAnaliziZorunluOlmali=@NedenAnaliziZorunluOlmali,Aciklama=@Aciklama,Silindi=@Silindi where ArizaNedeniID=@ArizaNedeniID", arizanedeni);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from ArizaNedeni where ArizaNedeniID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update ArizaNedeni set Silindi = 1 where ArizaNedeniID=@Id", new { Id });
        }
        public List<ArizaNedeni> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM ArizaNedeni where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM ArizaNedeni {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}