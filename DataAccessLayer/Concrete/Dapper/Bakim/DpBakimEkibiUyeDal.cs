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
using EntityLayer.Concrete.Genel;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpBakimEkibiUyeDal : DpEntityRepositoryBase<BakimEkibiUye>, IBakimEkibiUyeDal
    {
        public List<BakimEkibiUye> GetList()
        {
            return GetListQuery("select * from BakimEkibiUye where Silindi=0", new { });
        }

        public BakimEkibiUye Get(int Id)
        {
            return GetQuery("select * from BakimEkibiUye where BakimEkibiUyeID= @Id and Silindi=0", new { Id });
        }

        public int Add(BakimEkibiUye bakimekibiuye)
        {
            return AddQuery("insert into BakimEkibiUye(BakimEkibiID,KaynakID,Silindi) values (@BakimEkibiID,@KaynakID,@Silindi)", bakimekibiuye);
        }

        public int Update(BakimEkibiUye bakimekibiuye)
        {
            return UpdateQuery("update BakimEkibiUye set BakimEkibiID=@BakimEkibiID,KaynakID=@KaynakID,Silindi=@Silindi where BakimEkibiUyeID=@BakimEkibiUyeID", bakimekibiuye);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BakimEkibiUye where BakimEkibiUyeID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BakimEkibiUye set Silindi = 1 where BakimEkibiUyeID=@Id", new { Id });
        }

        public List<BakimEkibiUye> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM BakimEkibiUye where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BakimEkibiUye where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public int AddWithTransaction(int BakimEkibiID, List<int> listKaynakID)
        {
            var count = 0;
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                connection.Execute("update BakimEkibiUye set Silindi = 1 where BakimEkibiID=@BakimEkibiID", new { BakimEkibiID }, transaction);
                
                foreach (var kaynakID in listKaynakID)
                {
                    count += connection.Execute("insert into BakimEkibiUye(BakimEkibiID, KaynakID, Silindi) values(@BakimEkibiID, @KaynakID, @Silindi)", new BakimEkibiUye()
                    {
                        BakimEkibiID = BakimEkibiID,
                        KaynakID = kaynakID,
                        Silindi = false
                    }, transaction);

                }

                transaction.Commit();
            }
            return count;
        }

        public List<BakimEkibiUye> GetListByBakimEkibiID(int BakimEkibiID)
        {
            return GetListQuery("select * from BakimEkibiUye where BakimEkibiID= @BakimEkibiID and Silindi=0",
                new { BakimEkibiID });
        }
    }
}