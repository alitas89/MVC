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
    public class DpIsletmeDal : DpEntityRepositoryBase<Isletme>, IIsletmeDal
    {
        public List<Isletme> GetList()
        {
            return GetListQuery($"select * from Isletme where Silindi=0", new { });
        }

        public Isletme Get(int Id)
        {
            return GetQuery("select * from Isletme where IsletmeID= @Id and Silindi=0", new { Id });
        }

        public int Add(Isletme ısletme)
        {
            return AddQuery("insert into Isletme(Kod,Ad,HaritaResmiYolu,Aciklama) values (@Kod,@Ad,@HaritaResmiYolu,@Aciklama)", ısletme);
        }

        public int Update(Isletme ısletme)
        {
            return UpdateQuery("update Isletme set Kod=@Kod,Ad=@Ad,HaritaResmiYolu=@HaritaResmiYolu,Aciklama=@Aciklama where IsletmeID=@IsletmeID", ısletme);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Isletme where IsletmeID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            var Count = 0;
            var result = UpdateQuery(
                "select @Count = count (*) from Sarfyeri where IsletmeID=@Id " +
                "if(@Count = 0) "+
                "begin "     +
                "update Isletme set Silindi = 1 where IsletmeID = @Id " +
                "end "
                , new { Id ,Count}) + "";
            int.TryParse(result, out int Result);
            return Result;
        }
        public List<Isletme> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM Isletme where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Isletme where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public bool IsKodDefined(string Kod)
        {
            var result = GetScalarQuery("select Count(*) from Isletme where Kod= @Kod and Silindi=0", new { Kod }) + "";
            int.TryParse(result, out int count);
            return count > 0;
        }

        public List<string> AddListWithTransactionBySablon(List<Isletme> listIsletme)
        {
            List<string> listIsletmeID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var isletme in listIsletme)
                    {
                        var strIsletmeID = connection.ExecuteScalar("insert into Isletme(Kod,Ad,HaritaResmiYolu,Aciklama) values (@Kod,@Ad,@HaritaResmiYolu,@Aciklama);" +
                            "SELECT CAST(SCOPE_IDENTITY() as int)", isletme, transaction);
                        listIsletmeID.Add(strIsletmeID + "");
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listIsletmeID;
            }

        }
    }
}