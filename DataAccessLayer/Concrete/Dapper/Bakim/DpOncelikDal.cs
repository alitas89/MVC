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
    public class DpOncelikDal : DpEntityRepositoryBase<Oncelik>, IOncelikDal
    {
        public List<Oncelik> GetList()
        {
            return GetListQuery("select * from Oncelik where Silindi=0", new { });
        }

        public Oncelik Get(int Id)
        {
            return GetQuery("select * from Oncelik where OncelikID= @Id and Silindi=0", new { Id });
        }

        public int Add(Oncelik oncelik)
        {
            return AddQuery("insert into Oncelik(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", oncelik);
        }

        public int Update(Oncelik oncelik)
        {
            return UpdateQuery("update Oncelik set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where OncelikID=@OncelikID", oncelik);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Oncelik where OncelikID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Oncelik set Silindi = 1 where OncelikID=@Id", new { Id });
        }
        public List<Oncelik> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM Oncelik where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Oncelik where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<Oncelik> listOncelik)
        {
            List<string> listOncelikID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var oncelik in listOncelik)
                    {
                        var strOncelikID = connection.ExecuteScalar("insert into Oncelik(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", oncelik, transaction);

                        listOncelikID.Add(strOncelikID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listOncelikID;
            }
        }
    }
}