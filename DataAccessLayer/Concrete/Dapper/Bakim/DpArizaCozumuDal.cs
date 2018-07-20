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
    public class DpArizaCozumuDal : DpEntityRepositoryBase<ArizaCozumu>, IArizaCozumuDal
    {
        public List<ArizaCozumu> GetList()
        {
            return GetListQuery("select * from ArizaCozumu where Silindi=0", new { });
        }

        public ArizaCozumu Get(int Id)
        {
            return GetQuery("select * from ArizaCozumu where ArizaCozumuID= @Id and Silindi=0", new { Id });
        }

        public int Add(ArizaCozumu arizacozumu)
        {
            return AddQuery("insert into ArizaCozumu(Kod,Ad,TekNoktaEgitimiOlustur,Aciklama,Silindi) values (@Kod,@Ad,@TekNoktaEgitimiOlustur,@Aciklama,@Silindi)", arizacozumu);
        }

        public int Update(ArizaCozumu arizacozumu)
        {
            return UpdateQuery("update ArizaCozumu set Kod=@Kod,Ad=@Ad,TekNoktaEgitimiOlustur=@TekNoktaEgitimiOlustur,Aciklama=@Aciklama,Silindi=@Silindi where ArizaCozumuID=@ArizaCozumuID", arizacozumu);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from ArizaCozumu where ArizaCozumuID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update ArizaCozumu set Silindi = 1 where ArizaCozumuID=@Id", new { Id });
        }
        public List<ArizaCozumu> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM ArizaCozumu where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM ArizaCozumu where Silindi=0 {filterQuery} ", new { filterQuery }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<ArizaCozumu> listArizaCozumu)
        {
            List<string> listArizaCozumuID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var arizacozumu in listArizaCozumu)
                    {
                        var strArizaCozumuID = connection.ExecuteScalar("insert into ArizaCozumu(Kod,Ad,TekNoktaEgitimiOlustur,Aciklama) values (@Kod,@Ad,@TekNoktaEgitimiOlustur,@Aciklama);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", arizacozumu, transaction);

                        listArizaCozumuID.Add(strArizaCozumuID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listArizaCozumuID;
            }
        }

    }
}
