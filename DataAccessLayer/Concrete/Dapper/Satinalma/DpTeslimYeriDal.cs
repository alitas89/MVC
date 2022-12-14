using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;
using Core.Utilities.Dal;
using EntityLayer.ComplexTypes.DtoModel.SatinAlma;

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
              string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";

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

            return GetListQuery($@"SELECT {columnsQuery} FROM TeslimYeri where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM TeslimYeri where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }


        public List<TeslimYeriDto> GetListPaginationDto(PagingParams pagingParams)
        {
              string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";

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

            return new DpDtoRepositoryBase<TeslimYeriDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_TeslimYeriDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_TeslimYeriDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}