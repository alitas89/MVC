using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpBilgilendirmeTuruDal : DpEntityRepositoryBase<BilgilendirmeTuru>, IBilgilendirmeTuruDal
    {
        public List<BilgilendirmeTuru> GetList()
        {
            return GetListQuery("select * from BilgilendirmeTuru where Silindi=0", new { });
        }

        public BilgilendirmeTuru Get(int Id)
        {
            return GetQuery("select * from BilgilendirmeTuru where BilgilendirmeTuruID= @Id and Silindi=0", new { Id });
        }

        public int Add(BilgilendirmeTuru bilgilendirmeturu)
        {
            return AddQuery("insert into BilgilendirmeTuru(BilgilendirmeTuruAd) values (@BilgilendirmeTuruAd)", bilgilendirmeturu);
        }

        public int Update(BilgilendirmeTuru bilgilendirmeturu)
        {
            return UpdateQuery("update BilgilendirmeTuru set BilgilendirmeTuruAd=@BilgilendirmeTuruAd where BilgilendirmeTuruID=@BilgilendirmeTuruID", bilgilendirmeturu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BilgilendirmeTuru where BilgilendirmeTuruID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BilgilendirmeTuru set Silindi = 1 where BilgilendirmeTuruID=@Id", new { Id });
        }

        public List<BilgilendirmeTuru> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM BilgilendirmeTuru where Silindi=0 {filterQuery} {orderQuery}
              OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string filter = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                filter = $"and {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BilgilendirmeTuru where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}