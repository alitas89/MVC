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
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpArizaNedeniGrubuDal : DpEntityRepositoryBase<ArizaNedeniGrubu>, IArizaNedeniGrubuDal
    {
        public List<ArizaNedeniGrubu> GetList()
        {
            return GetListQuery("select * from ArizaNedeniGrubu where Silindi=0", new { });
        }

        public ArizaNedeniGrubu Get(int Id)
        {
            return GetQuery("select * from ArizaNedeniGrubu where ArizaNedeniGrubuID= @Id and Silindi=0", new { Id });
        }

        public int Add(ArizaNedeniGrubu arizanedenigrubu)
        {
            return AddQuery("insert into ArizaNedeniGrubu(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)", arizanedenigrubu);
        }

        public int Update(ArizaNedeniGrubu arizanedenigrubu)
        {
            return UpdateQuery("update ArizaNedeniGrubu set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where ArizaNedeniGrubuID=@ArizaNedeniGrubuID", arizanedenigrubu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from ArizaNedeniGrubu where ArizaNedeniGrubuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update ArizaNedeniGrubu set Silindi = 1 where ArizaNedeniGrubuID=@Id", new { Id });
        }

        public List<ArizaNedeniGrubu> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM ArizaNedeniGrubu where Silindi=0 {filterQuery} {orderQuery}
                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM ArizaNedeniGrubu where Silindi=0 {filterQuery} ", new { filterQuery }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<ArizaNedeniGrubu> listArizaNedeniGrubu)
        {
            List<string> listArizaNedeniGrubuID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var arizanedenigrubu in listArizaNedeniGrubu)
                    {
                        var strArizaNedeniGrubuID = connection.ExecuteScalar("insert into ArizaNedeniGrubu(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", arizanedenigrubu, transaction);

                        listArizaNedeniGrubuID.Add(strArizaNedeniGrubuID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listArizaNedeniGrubuID;
            }
        }

    }
}
