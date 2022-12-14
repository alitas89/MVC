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
    public class DpDurusNedeniDal : DpEntityRepositoryBase<DurusNedeni>, IDurusNedeniDal
    {
        public List<DurusNedeni> GetList()
        {
            return GetListQuery($"select * from DurusNedeni where Silindi=0", new { });
        }

        public DurusNedeni Get(int Id)
        {
            return GetQuery("select * from DurusNedeni where DurusNedeniID= @Id and Silindi=0", new { Id });
        }

        public int Add(DurusNedeni durusnedeni)
        {
            return AddQuery("insert into DurusNedeni(Kod,Ad,BakimDurusu,Aciklama) values (@Kod,@Ad,@BakimDurusu,@Aciklama)", durusnedeni);
        }

        public int Update(DurusNedeni durusnedeni)
        {
            return UpdateQuery("update DurusNedeni set Kod=@Kod,Ad=@Ad,BakimDurusu=@BakimDurusu,Aciklama=@Aciklama where DurusNedeniID=@DurusNedeniID", durusnedeni);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from DurusNedeni where DurusNedeniID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update DurusNedeni set Silindi = 1 where DurusNedeniID=@Id", new { Id });
        }
        public List<DurusNedeni> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM DurusNedeni where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM DurusNedeni where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<DurusNedeni> listDurusNedeni)
        {
            List<string> listDurusNedeniID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var durusnedeni in listDurusNedeni)
                    {
                        var strDurusNedeniID = connection.ExecuteScalar("insert into DurusNedeni(Kod,Ad,BakimDurusu,Aciklama) values (@Kod,@Ad,@BakimDurusu,@Aciklama);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", durusnedeni, transaction);

                        listDurusNedeniID.Add(strDurusNedeniID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listDurusNedeniID;
            }


        }
    }
}