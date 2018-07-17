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
    public class DpVarlikTuruDal : DpEntityRepositoryBase<VarlikTuru>, IVarlikTuruDal
    {
        public List<VarlikTuru> GetList()
        {
            return GetListQuery($"select * from VarlikTuru where Silindi=0", new { });
        }

        public VarlikTuru Get(int Id)
        {
            return GetQuery("select * from VarlikTuru where VarlikTuruID= @Id and Silindi=0", new { Id });
        }

        public int Add(VarlikTuru varlikturu)
        {
            return AddQuery("insert into VarlikTuru(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama)", varlikturu);
        }

        public int Update(VarlikTuru varlikturu)
        {
            return UpdateQuery("update VarlikTuru set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama where VarlikTuruID=@VarlikTuruID", varlikturu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from VarlikTuru where VarlikTuruID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update VarlikTuru set Silindi = 1 where VarlikTuruID=@Id", new { Id });
        }

        public List<VarlikTuru> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM VarlikTuru where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM VarlikTuru where Silindi=0 {filterQuery} ", new {  }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<VarlikTuru> listVarlikTuru)
        {
            List<string> listVarlikTuruID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var varlikturu in listVarlikTuru)
                    {
                        var strVarlikTuruID = connection.ExecuteScalar("insert into VarlikTuru(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", varlikturu, transaction);

                        listVarlikTuruID.Add(strVarlikTuruID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listVarlikTuruID;
            }

        }
    }
}