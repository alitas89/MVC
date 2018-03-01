using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace DataAccessLayer.Concrete.Dapper.Personel
{
    public class DpKaynakTuruDal : DpEntityRepositoryBase<KaynakTuru>, IKaynakTuruDal
    {
        public List<KaynakTuru> GetList()
        {
            return GetListQuery("select * from KaynakTuru where Silindi=0", new { });
        }

        public KaynakTuru Get(int Id)
        {
            return GetQuery("select * from KaynakTuru where KaynakTuruID= @Id and Silindi=0", new { Id });
        }

        public int Add(KaynakTuru kaynakturu)
        {
            return AddQuery("insert into KaynakTuru(Ad,Silindi) values (@Ad,@Silindi)", kaynakturu);
        }

        public int Update(KaynakTuru kaynakturu)
        {
            return UpdateQuery("update KaynakTuru set Ad=@Ad,Silindi=@Silindi where KaynakTuruID=@KaynakTuruID", kaynakturu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from KaynakTuru where KaynakTuruID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update KaynakTuru set Silindi = 1 where KaynakTuruID=@Id", new { Id });
        }

        public List<KaynakTuru> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM KaynakTuru where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM KaynakTuru {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}