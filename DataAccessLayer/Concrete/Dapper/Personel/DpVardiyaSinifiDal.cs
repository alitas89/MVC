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
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace DataAccessLayer.Concrete.Dapper.Personel
{
    public class DpVardiyaSinifiDal : DpEntityRepositoryBase<VardiyaSinifi>, IVardiyaSinifiDal
    {
        public List<VardiyaSinifi> GetList()
        {
            return GetListQuery("select * from VardiyaSinifi where Silindi=0", new { });
        }

        public VardiyaSinifi Get(int Id)
        {
            return GetQuery("select * from VardiyaSinifi where VardiyaSinifiID= @Id and Silindi=0", new {Id});
        }

        public int Add(VardiyaSinifi vardiyasinifi)
        {
            return AddQuery("insert into VardiyaSinifi(Kod,Ad,Aciklama,Silindi) values (@Kod,@Ad,@Aciklama,@Silindi)",
                vardiyasinifi);
        }

        public int Update(VardiyaSinifi vardiyasinifi)
        {
            return UpdateQuery(
                "update VardiyaSinifi set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,Silindi=@Silindi where VardiyaSinifiID=@VardiyaSinifiID",
                vardiyasinifi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from VardiyaSinifi where VardiyaSinifiID=@Id ", new {Id});
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update VardiyaSinifi set Silindi = 1 where VardiyaSinifiID=@Id", new {Id});
        }

        public List<VardiyaSinifi> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM VardiyaSinifi where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new {pagingParams.filter, pagingParams.offset, pagingParams.limit});
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM VardiyaSinifi where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<VardiyaSinifi> listVardiyaSinifi)
        {
            List<string> listVardiyaSinifiID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var vardiyasinifi in listVardiyaSinifi)
                    {
                        var strVardiyaSinifiID = connection.ExecuteScalar("insert into VardiyaSinifi(Kod,Ad,Aciklama) values (@Kod,@Ad,@Aciklama);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", vardiyasinifi, transaction);

                        listVardiyaSinifiID.Add(strVardiyaSinifiID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listVardiyaSinifiID;
            }
        }
    }
}
