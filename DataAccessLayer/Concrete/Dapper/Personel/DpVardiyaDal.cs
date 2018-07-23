using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using Dapper;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace DataAccessLayer.Concrete.Dapper.Personel
{
    public class DpVardiyaDal : DpEntityRepositoryBase<Vardiya>, IVardiyaDal
    {
        public List<Vardiya> GetList()
        {
            return GetListQuery("select * from Vardiya where Silindi=0", new { });
        }

        public Vardiya Get(int Id)
        {
            return GetQuery("select * from Vardiya where VardiyaID= @Id and Silindi=0", new { Id });
        }

        public int Add(Vardiya vardiya)
        {
            return AddQuery("insert into Vardiya(Kod,Ad,BaslangicSaati,BaslangicSaati2,BitisSaati,BitisSaati2,SarfYeriID,BakimSuresiHesabinaDahil,DurusSuresiHesabinaDahil,Silindi) values (@Kod,@Ad,@BaslangicSaati,@BaslangicSaati2,@BitisSaati,@BitisSaati2,@SarfYeriID,@BakimSuresiHesabinaDahil,@DurusSuresiHesabinaDahil,@Silindi)", vardiya);
        }

        public int Update(Vardiya vardiya)
        {
            return UpdateQuery("update Vardiya set Kod=@Kod,Ad=@Ad,BaslangicSaati=@BaslangicSaati,BaslangicSaati2=@BaslangicSaati2,BitisSaati=@BitisSaati,BitisSaati2=@BitisSaati2,SarfYeriID=@SarfYeriID,BakimSuresiHesabinaDahil=@BakimSuresiHesabinaDahil,DurusSuresiHesabinaDahil=@DurusSuresiHesabinaDahil,Silindi=@Silindi where VardiyaID=@VardiyaID", vardiya);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Vardiya where VardiyaID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Vardiya set Silindi = 1 where VardiyaID=@Id", new { Id });
        }

        public List<Vardiya> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM Vardiya where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Vardiya where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<VardiyaDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<VardiyaDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_VardiyaDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_VardiyaDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<Vardiya> listVardiya)
        {
            List<string> listVardiyaID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var vardiya in listVardiya)
                    {
                        var strVardiyaID = connection.ExecuteScalar("insert into Vardiya(Kod,Ad,BaslangicSaati,BaslangicSaati2,BitisSaati,BitisSaati2,SarfYeriID,BakimSuresiHesabinaDahil,DurusSuresiHesabinaDahil) values (@Kod,@Ad,@BaslangicSaati,@BaslangicSaati2,@BitisSaati,@BitisSaati2,@SarfYeriID,@BakimSuresiHesabinaDahil,@DurusSuresiHesabinaDahil);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", vardiya, transaction);

                        listVardiyaID.Add(strVardiyaID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listVardiyaID;
            }
        }
    }
}
