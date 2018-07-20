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
    public class DpHizmetDal : DpEntityRepositoryBase<Hizmet>, IHizmetDal
    {
        public List<Hizmet> GetList()
        {
            return GetListQuery("select * from Hizmet where Silindi=0", new { });
        }

        public Hizmet Get(int Id)
        {
            return GetQuery("select * from Hizmet where HizmetID= @Id and Silindi=0", new { Id });
        }

        public int Add(Hizmet hizmet)
        {
            return AddQuery("insert into Hizmet(Kod,Ad,BirimFiyat,ParaBirimID,Aciklama,Silindi) values (@Kod,@Ad,@BirimFiyat,@ParaBirimID,@Aciklama,@Silindi)", hizmet);
        }

        public int Update(Hizmet hizmet)
        {
            return UpdateQuery("update Hizmet set Kod=@Kod,Ad=@Ad,BirimFiyat=@BirimFiyat,ParaBirimID=@ParaBirimID,Aciklama=@Aciklama,Silindi=@Silindi where HizmetID=@HizmetID", hizmet);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Hizmet where HizmetID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Hizmet set Silindi = 1 where HizmetID=@Id", new { Id });
        }

        public List<Hizmet> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM Hizmet where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Hizmet where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<Hizmet> listHizmet)
        {
            List<string> listHizmetID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var hizmet in listHizmet)
                    {
                        var strHizmetID = connection.ExecuteScalar("insert into Hizmet(Kod,Ad,BirimFiyat,ParaBirimID,Aciklama) values (@Kod,@Ad,@BirimFiyat,@ParaBirimID,@Aciklama);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", hizmet, transaction);

                        listHizmetID.Add(strHizmetID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listHizmetID;
            }
        }
    }
}