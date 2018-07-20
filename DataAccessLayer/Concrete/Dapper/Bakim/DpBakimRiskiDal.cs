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
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpBakimRiskiDal : DpEntityRepositoryBase<BakimRiski>, IBakimRiskiDal
    {
        public List<BakimRiski> GetList()
        {
            return GetListQuery("select * from BakimRiski where Silindi=0", new { });
        }

        public BakimRiski Get(int Id)
        {
            return GetQuery("select * from BakimRiski where BakimRiskiID= @Id and Silindi=0", new { Id });
        }

        public int Add(BakimRiski bakimriski)
        {
            return AddQuery("insert into BakimRiski(RiskTipiID,Kod,Ad,Formulu,StokNo,Telefon,Aciklama1,Aciklama2,Aciklama3,Silindi) values (@RiskTipiID,@Kod,@Ad,@Formulu,@StokNo,@Telefon,@Aciklama1,@Aciklama2,@Aciklama3,@Silindi)", bakimriski);
        }

        public int Update(BakimRiski bakimriski)
        {
            return UpdateQuery("update BakimRiski set RiskTipiID=@RiskTipiID,Kod=@Kod,Ad=@Ad,Formulu=@Formulu,StokNo=@StokNo,Telefon=@Telefon,Aciklama1=@Aciklama1,Aciklama2=@Aciklama2,Aciklama3=@Aciklama3,Silindi=@Silindi where BakimRiskiID=@BakimRiskiID", bakimriski);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from BakimRiski where BakimRiskiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update BakimRiski set Silindi = 1 where BakimRiskiID=@Id", new { Id });
        }

        public List<BakimRiski> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM BakimRiski where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM BakimRiski where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<BakimRiskiDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<BakimRiskiDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_BakimRiskiDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_BakimRiskiDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<BakimRiski> GetListBakimRiskiByPeriyodikBakimID(int PeriyodikBakimID)
        {
            return GetListQuery("select * from BakimRiski where BakimRiskiID " +
                                "in(select BakimRiskiID from BakimRiskiAraTablo where PeriyodikBakimID= @PeriyodikBakimID and Silindi=0)",
                                new { PeriyodikBakimID });
        }

        public List<string> AddListWithTransactionBySablon(List<BakimRiski> listBakimRiski)
        {
            List<string> listBakimRiskiID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var bakimriski in listBakimRiski)
                    {
                        var strBakimRiskiID = connection.ExecuteScalar("insert into BakimRiski(RiskTipiID,Kod,Ad,Formulu,StokNo,Telefon,Aciklama1,Aciklama2,Aciklama3) values (@RiskTipiID,@Kod,@Ad,@Formulu,@StokNo,@Telefon,@Aciklama1,@Aciklama2,@Aciklama3);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", bakimriski, transaction);

                        listBakimRiskiID.Add(strBakimRiskiID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listBakimRiskiID;
            }
        }
    }
}