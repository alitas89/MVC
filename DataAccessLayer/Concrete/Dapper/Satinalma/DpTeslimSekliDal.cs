using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpTeslimSekliDal : DpEntityRepositoryBase<TeslimSekli>, ITeslimSekliDal
    {
        public List<TeslimSekli> GetList()
        {
            return GetListQuery("select * from TeslimSekli where Silindi=0", new { });
        }

        public TeslimSekli Get(int Id)
        {
            return GetQuery("select * from TeslimSekli where TeslimSekliID= @Id and Silindi=0", new { Id });
        }

        public int Add(TeslimSekli teslimsekli)
        {
            return AddQuery("insert into TeslimSekli(Kod,Ad,Aciklama,VarsayilanDeger,Silindi) values (@Kod,@Ad,@Aciklama,@VarsayilanDeger,@Silindi)", teslimsekli);
        }

        public int Update(TeslimSekli teslimsekli)
        {
            return UpdateQuery("update TeslimSekli set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,VarsayilanDeger=@VarsayilanDeger,Silindi=@Silindi where TeslimSekliID=@TeslimSekliID", teslimsekli);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from TeslimSekli where TeslimSekliID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update TeslimSekli set Silindi = 1 where TeslimSekliID=@Id", new { Id });
        }

        public List<TeslimSekli> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM TeslimSekli where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM TeslimSekli {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}