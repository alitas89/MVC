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
    public class DpBakimOncelikDal : DpEntityRepositoryBase<BakimOncelik>, IBakimOncelikDal
    {
        public List<BakimOncelik> GetList()
        {
            return GetListQuery("select * from BakimOncelik where Silindi=0", new { });
        }

        public BakimOncelik Get(int Id)
        {
            return GetQuery("select * from BakimOncelik where BakimOncelikID= @Id and Silindi=0", new { Id });
        }

        public int Add(BakimOncelik bakimoncelik)
        {
            return AddQuery("insert into BakimOncelik(Kod,Ad,TamamlanmaZamani,BirimID,Aciklama,TeminSureleriID,IsEmriVarsayilani,IsTalepVarsayilani,PeriyodikBakimVarsayilani,Silindi) values (@Kod,@Ad,@TamamlanmaZamani,@BirimID,@Aciklama,@TeminSureleriID,@IsEmriVarsayilani,@IsTalepVarsayilani,@PeriyodikBakimVarsayilani,@Silindi)", bakimoncelik);
        }

        public int Update(BakimOncelik bakimoncelik)
        {
            return UpdateQuery("update BakimOncelik set Kod=@Kod,Ad=@Ad,TamamlanmaZamani=@TamamlanmaZamani,BirimID=@BirimID,Aciklama=@Aciklama,TeminSureleriID=@TeminSureleriID,IsEmriVarsayilani=@IsEmriVarsayilani,IsTalepVarsayilani=@IsTalepVarsayilani,PeriyodikBakimVarsayilani=@PeriyodikBakimVarsayilani,Silindi=@Silindi where BakimOncelikID=@BakimOncelikID", bakimoncelik);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BakimOncelik where BakimOncelikID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BakimOncelik set Silindi = 1 where BakimOncelikID=@Id", new { Id });
        }
        public List<BakimOncelik> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM BakimOncelik where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BakimOncelik where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<BakimOncelik> listBakimOncelik)
        {
            List<string> listBakimOncelikID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var bakimoncelik in listBakimOncelik)
                    {
                        var strBakimOncelikID = connection.ExecuteScalar("insert into BakimOncelik(Kod,Ad,TamamlanmaZamani,BirimID,Aciklama,TeminSureleriID,IsEmriVarsayilani,IsTalepVarsayilani,PeriyodikBakimVarsayilani) values (@Kod,@Ad,@TamamlanmaZamani,@BirimID,@Aciklama,@TeminSureleriID,@IsEmriVarsayilani,@IsTalepVarsayilani,@PeriyodikBakimVarsayilani);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", bakimoncelik, transaction);

                        listBakimOncelikID.Add(strBakimOncelikID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listBakimOncelikID;
            }
        }

    }
}