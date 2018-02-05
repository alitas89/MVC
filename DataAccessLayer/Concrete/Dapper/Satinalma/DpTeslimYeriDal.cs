using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;

namespace DataAccessLayer.Concrete.Dapper.Satinalma
{
    public class DpTeslimYeriDal : DpEntityRepositoryBase<TeslimYeri>, ITeslimYeriDal
    {
        public List<TeslimYeri> GetList()
        {
            return GetListQuery("select * from TeslimYeri where Silindi=0", new { });
        }

        public TeslimYeri Get(int Id)
        {
            return GetQuery("select * from TeslimYeri where TeslimYeriID= @Id and Silindi=0", new { Id });
        }

        public int Add(TeslimYeri teslimyeri)
        {
            return AddQuery("insert into TeslimYeri(Kod,Ad,KisimID,SarfYeriID,IsletmeID,Semt,Sehir,Ulke,Telefon1,Telefon2,Adres,Silindi) values (@Kod,@Ad,@KisimID,@SarfYeriID,@IsletmeID,@Semt,@Sehir,@Ulke,@Telefon1,@Telefon2,@Adres,@Silindi)", teslimyeri);
        }

        public int Update(TeslimYeri teslimyeri)
        {
            return UpdateQuery("update TeslimYeri set Kod=@Kod,Ad=@Ad,KisimID=@KisimID,SarfYeriID=@SarfYeriID,IsletmeID=@IsletmeID,Semt=@Semt,Sehir=@Sehir,Ulke=@Ulke,Telefon1=@Telefon1,Telefon2=@Telefon2,Adres=@Adres,Silindi=@Silindi where TeslimYeriID=@TeslimYeriID", teslimyeri);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from TeslimYeri where TeslimYeriID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update TeslimYeri set Silindi = 1 where TeslimYeriID=@Id", new { Id });
        }

        public List<TeslimYeri> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM TeslimYeri where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM TeslimYeri {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}