using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpBakimEkibiDal : DpEntityRepositoryBase<BakimEkibi>, IBakimEkibiDal
    {
        public List<BakimEkibi> GetList()
        {
            return GetListQuery("select * from BakimEkibi where Silindi=0", new { });
        }

        public BakimEkibi Get(int Id)
        {
            return GetQuery("select * from BakimEkibi where BakimEkibiID= @Id and Silindi=0", new { Id });
        }

        public int Add(BakimEkibi bakimekibi)
        {
            return AddQuery("insert into BakimEkibi(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", bakimekibi);
        }

        public int Update(BakimEkibi bakimekibi)
        {
            return UpdateQuery("update BakimEkibi set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where BakimEkibiID=@BakimEkibiID", bakimekibi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BakimEkibi where BakimEkibiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BakimEkibi set Silindi = 1 where BakimEkibiID=@Id", new { Id });
        }
        public List<BakimEkibi> GetListPagination(PagingParams pagingParams)
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

            //columns ayrımı yapılır
            string columnsQuery = "*";
            if (pagingParams.columns.Length != 0)
            {
                columnsQuery = pagingParams.columns;
            }

            return GetListQuery($@"SELECT {columnsQuery} FROM BakimEkibi where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BakimEkibi where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}