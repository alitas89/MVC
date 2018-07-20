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
    public class DpBeklemeIptalNedeniDal : DpEntityRepositoryBase<BeklemeIptalNedeni>, IBeklemeIptalNedeniDal
    {
        public List<BeklemeIptalNedeni> GetList()
        {
            return GetListQuery("select * from BeklemeIptalNedeni where Silindi=0", new { });
        }

        public BeklemeIptalNedeni Get(int Id)
        {
            return GetQuery("select * from BeklemeIptalNedeni where BeklemeIptalNedeniID= @Id and Silindi=0", new { Id });
        }

        public int Add(BeklemeIptalNedeni beklemeıptalnedeni)
        {
            return AddQuery("insert into BeklemeIptalNedeni(Kod,Ad,Aciklama,IsEmriniKapsayanPeriyodikBakimOlustur,IptalEdilenOtonomBakimdanIsEmriOlustur,Silindi) values (@Kod,@Ad,@Aciklama,@IsEmriniKapsayanPeriyodikBakimOlustur,@IptalEdilenOtonomBakimdanIsEmriOlustur,@Silindi)", beklemeıptalnedeni);
        }

        public int Update(BeklemeIptalNedeni beklemeıptalnedeni)
        {
            return UpdateQuery("update BeklemeIptalNedeni set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,IsEmriniKapsayanPeriyodikBakimOlustur=@IsEmriniKapsayanPeriyodikBakimOlustur,IptalEdilenOtonomBakimdanIsEmriOlustur=@IptalEdilenOtonomBakimdanIsEmriOlustur,Silindi=@Silindi where BeklemeIptalNedeniID=@BeklemeIptalNedeniID", beklemeıptalnedeni);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BeklemeIptalNedeni where BeklemeIptalNedeniID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BeklemeIptalNedeni set Silindi = 1 where BeklemeIptalNedeniID=@Id", new { Id });
        }
        public List<BeklemeIptalNedeni> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM BeklemeIptalNedeni where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BeklemeIptalNedeni where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<BeklemeIptalNedeni> listBeklemeIptalNedeni)
        {
            List<string> listBeklemeIptalNedeniID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var beklemeıptalnedeni in listBeklemeIptalNedeni)
                    {
                        var strBeklemeIptalNedeniID = connection.ExecuteScalar("insert into BeklemeIptalNedeni(Kod,Ad,Aciklama,IsEmriniKapsayanPeriyodikBakimOlustur,IptalEdilenOtonomBakimdanIsEmriOlustur) values (@Kod,@Ad,@Aciklama,@IsEmriniKapsayanPeriyodikBakimOlustur,@IptalEdilenOtonomBakimdanIsEmriOlustur);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", beklemeıptalnedeni, transaction);

                        listBeklemeIptalNedeniID.Add(strBeklemeIptalNedeniID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listBeklemeIptalNedeniID;
            }
        }
    }
}