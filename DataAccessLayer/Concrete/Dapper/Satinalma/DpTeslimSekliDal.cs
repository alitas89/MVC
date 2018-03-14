using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;
using Core.Utilities.Dal;

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

            return GetListQuery($@"SELECT {columnsQuery} FROM TeslimSekli where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM TeslimSekli where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}