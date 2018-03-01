using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace DataAccessLayer.Concrete.Dapper.Personel
{
    public class DpKaynakDurumuDal : DpEntityRepositoryBase<KaynakDurumu>, IKaynakDurumuDal
    {
        public List<KaynakDurumu> GetList()
        {
            return GetListQuery("select * from KaynakDurumu where Silindi=0", new { });
        }

        public KaynakDurumu Get(int Id)
        {
            return GetQuery("select * from KaynakDurumu where KaynakDurumuID= @Id and Silindi=0", new { Id });
        }

        public int Add(KaynakDurumu kaynakdurumu)
        {
            return AddQuery("insert into KaynakDurumu(Ad,Silindi) values (@Ad,@Silindi)", kaynakdurumu);
        }

        public int Update(KaynakDurumu kaynakdurumu)
        {
            return UpdateQuery("update KaynakDurumu set Ad=@Ad,Silindi=@Silindi where KaynakDurumuID=@KaynakDurumuID", kaynakdurumu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from KaynakDurumu where KaynakDurumuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update KaynakDurumu set Silindi = 1 where KaynakDurumuID=@Id", new { Id });
        }

        public List<KaynakDurumu> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM KaynakDurumu where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM KaynakDurumu {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}