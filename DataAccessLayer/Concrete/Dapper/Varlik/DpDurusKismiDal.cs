using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using Dapper;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpDurusKismiDal : DpEntityRepositoryBase<DurusKismi>, IDurusKismiDal
    {
        public List<DurusKismi> GetList()
        {
            return GetListQuery($"select * from DurusKismi where Silindi=0", new { });
        }

        public DurusKismi Get(int Id)
        {
            return GetQuery("select * from DurusKismi where DurusKismiID= @Id and Silindi=0", new { Id });
        }

        public int Add(DurusKismi duruskismi)
        {
            return AddQuery("insert into DurusKismi(Kod,Ad,BakimDurusu,Aciklama) values (@Kod,@Ad,@BakimDurusu,@Aciklama)", duruskismi);
        }

        public int Update(DurusKismi duruskismi)
        {
            return UpdateQuery("update DurusKismi set Kod=@Kod,Ad=@Ad,BakimDurusu=@BakimDurusu,Aciklama=@Aciklama where DurusKismiID=@DurusKismiID", duruskismi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from DurusKismi where DurusKismiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update DurusKismi set Silindi = 1 where DurusKismiID=@Id", new { Id });
        }
        public List<DurusKismi> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM DurusKismi where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM DurusKismi where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<DurusKismi> listDurusKismi)
        {
            List<string> listDurusKismiID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var duruskismi in listDurusKismi)
                    {
                        var strDurusKismiID = connection.ExecuteScalar("insert into DurusKismi(Kod,Ad,BakimDurusu,Aciklama) values (@Kod,@Ad,@BakimDurusu,@Aciklama);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", duruskismi, transaction);

                        listDurusKismiID.Add(strDurusKismiID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listDurusKismiID;
            }


        }
    }
}