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
    public class DpUrunDal : DpEntityRepositoryBase<Urun>, IUrunDal
    {
        public List<Urun> GetList()
        {
            return GetListQuery($"select * from Urun where Silindi=0", new { });
        }

        public Urun Get(int Id)
        {
            return GetQuery("select * from Urun where UrunID= @Id and Silindi=0", new { Id });
        }

        public int Add(Urun urun)
        {
            return AddQuery("insert into Urun(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama)", urun);
        }

        public int Update(Urun urun)
        {
            return UpdateQuery("update Urun set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama where UrunID=@UrunID", urun);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Urun where UrunID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Urun set Silindi = 1 where UrunID=@Id", new { Id });
        }

        public List<Urun> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM Urun where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Urun where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<Urun> listUrun)
        {
            List<string> listUrunID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var urun in listUrun)
                    {
                        var strUrunID = connection.ExecuteScalar("insert into Urun(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", urun, transaction);

                        listUrunID.Add(strUrunID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listUrunID;
            }
        }
    }
}