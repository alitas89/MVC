using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
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

            return GetListQuery($@"SELECT {columnsQuery} FROM GonderimFormati where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM GonderimFormati where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }


    }
}