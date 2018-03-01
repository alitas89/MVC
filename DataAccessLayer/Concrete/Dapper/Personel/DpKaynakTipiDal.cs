using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace DataAccessLayer.Concrete.Dapper.Personel
{
    public class DpKaynakTipiDal : DpEntityRepositoryBase<KaynakTipi>, IKaynakTipiDal
    {
        public List<KaynakTipi> GetList()
        {
            return GetListQuery("select * from KaynakTipi where Silindi=0", new { });
        }

        public KaynakTipi Get(int Id)
        {
            return GetQuery("select * from KaynakTipi where KaynakTipiID= @Id and Silindi=0", new { Id });
        }

        public int Add(KaynakTipi kaynaktipi)
        {
            return AddQuery("insert into KaynakTipi(Ad,Silindi) values (@Ad,@Silindi)", kaynaktipi);
        }

        public int Update(KaynakTipi kaynaktipi)
        {
            return UpdateQuery("update KaynakTipi set Ad=@Ad,Silindi=@Silindi where KaynakTipiID=@KaynakTipiID", kaynaktipi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from KaynakTipi where KaynakTipiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update KaynakTipi set Silindi = 1 where KaynakTipiID=@Id", new { Id });
        }

        public List<KaynakTipi> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM KaynakTipi where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM KaynakTipi {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}