using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpGonderimFormatiDal : DpEntityRepositoryBase<GonderimFormati>, IGonderimFormatiDal
    {
        public List<GonderimFormati> GetList()
        {
            return GetListQuery("select * from GonderimFormati where Silindi=0", new { });
        }

        public GonderimFormati Get(int Id)
        {
            return GetQuery("select * from GonderimFormati where GonderimFormatiID= @Id and Silindi=0", new { Id });
        }

        public int Add(GonderimFormati gonderimformati)
        {
            return AddQuery("insert into GonderimFormati(GonderimTuruID,Kod,Ad,Konu,Format,Silindi) values (@GonderimTuruID,@Kod,@Ad,@Konu,@Format,@Silindi)", gonderimformati);
        }

        public int Update(GonderimFormati gonderimformati)
        {
            return UpdateQuery("update GonderimFormati set GonderimTuruID=@GonderimTuruID,Kod=@Kod,Ad=@Ad,Konu=@Konu,Format=@Format,Silindi=@Silindi where GonderimFormatiID=@GonderimFormatiID", gonderimformati);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from GonderimFormati where GonderimFormatiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update GonderimFormati set Silindi = 1 where GonderimFormatiID=@Id", new { Id });
        }
        public List<GonderimFormati> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM GonderimFormati where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM GonderimFormati {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}