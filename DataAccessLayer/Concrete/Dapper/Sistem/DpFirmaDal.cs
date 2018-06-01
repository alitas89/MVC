using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Concrete.Dapper.Sistem
{
    public class DpFirmaDal : DpEntityRepositoryBase<Firma>, IFirmaDal
    {
        public List<Firma> GetList()
        {
            return GetListQuery("select * from Firma where Silindi=0", new { });
        }

        public Firma Get(int Id)
        {
            return GetQuery("select * from Firma where FirmaID= @Id and Silindi=0", new { Id });
        }

        public int Add(Firma firma)
        {
            return AddQuery("insert into Firma(Ad,Kod,Sorumlu,Adres,Telefon,Silindi) values (@Ad,@Kod,@Sorumlu,@Adres,@Telefon,@Silindi)", firma);
        }

        public int Update(Firma firma)
        {
            return UpdateQuery("update Firma set Ad=@Ad,Kod=@Kod,Sorumlu=@Sorumlu,Adres=@Adres,Telefon=@Telefon,Silindi=@Silindi where FirmaID=@FirmaID", firma);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Firma where FirmaID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Firma set Silindi = 1 where FirmaID=@Id", new { Id });
        }

        public List<Firma> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM Firma where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Firma where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

    }
}