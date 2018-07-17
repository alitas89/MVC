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
    public class DpMarkaDal : DpEntityRepositoryBase<Marka>, IMarkaDal
    {
        public List<Marka> GetList()
        {
            return GetListQuery($"select * from Marka where Silindi=0", new { });
        }

        public Marka Get(int Id)
        {
            return GetQuery("select * from Marka where MarkaID= @Id and Silindi=0", new { Id });
        }

        public int Add(Marka marka)
        {
            return AddQuery("insert into Marka(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama)", marka);
        }

        public int Update(Marka marka)
        {
            return UpdateQuery("update Marka set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama where MarkaID=@MarkaID", marka);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Marka where MarkaID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Marka set Silindi = 1 where MarkaID=@Id", new { Id });
        }
        public List<Marka> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM Marka where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Marka where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<Marka> listMarka)
        {
            List<string> listMarkaID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var marka in listMarka)
                    {
                        var strMarkaID = connection.ExecuteScalar("insert into Marka(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", marka, transaction);

                        listMarkaID.Add(strMarkaID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listMarkaID;
            }
        }
}
}