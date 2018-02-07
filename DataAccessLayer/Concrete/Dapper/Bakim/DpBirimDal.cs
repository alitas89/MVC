using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpBirimDal : DpEntityRepositoryBase<Birim>, IBirimDal
    {
        public List<Birim> GetList()
        {
            return GetListQuery("select * from Birim where Silindi=0", new { });
        }

        public Birim Get(int Id)
        {
            return GetQuery("select * from Birim where BirimID= @Id and Silindi=0", new { Id });
        }

        public int Add(Birim birim)
        {
            return AddQuery("insert into Birim(BirimAd) values (@BirimAd)", birim);
        }

        public int Update(Birim birim)
        {
            return UpdateQuery("update Birim set BirimAd=@BirimAd where BirimID=@BirimID", birim);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Birim where BirimID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Birim set Silindi = 1 where BirimID=@Id", new { Id });
        }
        public List<Birim> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Birim where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Birim where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}
