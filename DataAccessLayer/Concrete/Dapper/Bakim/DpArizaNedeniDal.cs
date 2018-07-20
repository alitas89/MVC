using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
    public class DpArizaNedeniDal : DpEntityRepositoryBase<ArizaNedeni>, IArizaNedeniDal
    {
        public List<ArizaNedeni> GetList()
        {
            return GetListQuery("select * from ArizaNedeni where Silindi=0", new { });
        }

        public ArizaNedeni Get(int Id)
        {
            return GetQuery("select * from ArizaNedeni where ArizaNedeniID= @Id and Silindi=0", new { Id });
        }

        public int Add(ArizaNedeni arizanedeni)
        {
            return AddQuery("insert into ArizaNedeni(Kod,GenelKod,Ad,UretimiDurdurur,NedenAnaliziZorunluOlmali,Aciklama,Silindi) values (@Kod,@GenelKod,@Ad,@UretimiDurdurur,@NedenAnaliziZorunluOlmali,@Aciklama,@Silindi)", arizanedeni);
        }

        public int Update(ArizaNedeni arizanedeni)
        {
            return UpdateQuery("update ArizaNedeni set Kod=@Kod,GenelKod=@GenelKod,Ad=@Ad,UretimiDurdurur=@UretimiDurdurur,NedenAnaliziZorunluOlmali=@NedenAnaliziZorunluOlmali,Aciklama=@Aciklama,Silindi=@Silindi where ArizaNedeniID=@ArizaNedeniID", arizanedeni);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from ArizaNedeni where ArizaNedeniID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update ArizaNedeni set Silindi = 1 where ArizaNedeniID=@Id", new { Id });
        }
        public List<ArizaNedeni> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM ArizaNedeni where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM ArizaNedeni where Silindi=0 {filterQuery} ", new { filterQuery }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<ArizaNedeni> listArizaNedeni)
        {
            List<string> listArizaNedeniID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var arizanedeni in listArizaNedeni)
                    {
                        var strArizaNedeniID = connection.ExecuteScalar("insert into ArizaNedeni(Kod,GenelKod,Ad,UretimiDurdurur,NedenAnaliziZorunluOlmali,Aciklama) values (@Kod,@GenelKod,@Ad,@UretimiDurdurur,@NedenAnaliziZorunluOlmali,@Aciklama);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", arizanedeni, transaction);

                        listArizaNedeniID.Add(strArizaNedeniID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listArizaNedeniID;
            }
        }
    }
}