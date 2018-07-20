using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using Dapper;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpGecikmeNedeniDal : DpEntityRepositoryBase<GecikmeNedeni>, IGecikmeNedeniDal
    {
        public List<GecikmeNedeni> GetList()
        {
            return GetListQuery("select * from GecikmeNedeni where Silindi=0", new { });
        }

        public GecikmeNedeni Get(int Id)
        {
            return GetQuery("select * from GecikmeNedeni where GecikmeNedeniID= @Id and Silindi=0", new { Id });
        }

        public int Add(GecikmeNedeni gecikmenedeni)
        {
            return AddQuery("insert into GecikmeNedeni(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", gecikmenedeni);
        }

        public int Update(GecikmeNedeni gecikmenedeni)
        {
            return UpdateQuery("update GecikmeNedeni set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where GecikmeNedeniID=@GecikmeNedeniID", gecikmenedeni);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from GecikmeNedeni where GecikmeNedeniID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update GecikmeNedeni set Silindi = 1 where GecikmeNedeniID=@Id", new { Id });
        }

        public List<GecikmeNedeni> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM GecikmeNedeni where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM GecikmeNedeni where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<GecikmeNedeni> listGecikmeNedeni)
        {
            List<string> listGecikmeNedeniID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var gecikmenedeni in listGecikmeNedeni)
                    {
                        var strGecikmeNedeniID = connection.ExecuteScalar("insert into GecikmeNedeni(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", gecikmenedeni, transaction);

                        listGecikmeNedeniID.Add(strGecikmeNedeniID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listGecikmeNedeniID;
            }
        }
    }
}