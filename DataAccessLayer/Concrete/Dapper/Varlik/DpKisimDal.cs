using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using Dapper;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Others;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpKisimDal : DpEntityRepositoryBase<Kisim>, IKisimDal
    {
        public List<Kisim> GetList()
        {
            return GetListQuery($"select * from Kisim where Silindi=0", new { });
        }

        public List<Kisim> GetList(int SarfYeriID)
        {
            return GetListQuery($"select * from Kisim where Silindi=0 and SarfYeriID=@SarfYeriID", new { SarfYeriID });
        }

        public Kisim Get(int Id)
        {
            return GetQuery("select * from Kisim where KisimID= @Id and Silindi=0", new { Id });
        }

        public int Add(Kisim kisim)
        {
            return AddQuery("insert into Kisim(Kod,Ad,Butce,HedeflenenButce,VardiyaSinifID,SarfYeriID,Aciklama) values (@Kod,@Ad,@Butce,@HedeflenenButce,@VardiyaSinifID,@SarfYeriID,@Aciklama); " +
                " SELECT CAST(SCOPE_IDENTITY() as int)", kisim, true);
        }

        public int Update(Kisim kisim)
        {
            return UpdateQuery("update Kisim set Kod=@Kod,Ad=@Ad,Butce=@Butce,HedeflenenButce=@HedeflenenButce,VardiyaSinifID=@VardiyaSinifID,SarfYeriID=@SarfYeriID,Aciklama=@Aciklama where KisimID=@KisimID", kisim);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Kisim where KisimID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Kisim set Silindi = 1 where KisimID=@Id", new { Id });
        }

        public List<KisimDto> GetListDto()
        {
            return new DpDtoRepositoryBase<KisimDto>().GetListDtoQuery("select KM.*, SY.Ad as SarfYeriAd from kisim KM left join SarfYeri SY on KM.SarfYeriID = SY.SarfYeriID where KM.Silindi=0", new { });
        }

        public List<Kisim> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM Kisim where Silindi=0 {filterQuery} {orderQuery}
                                                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public List<KisimDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<KisimDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_KisimDto where Silindi=0 {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Kisim where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public bool IsKodDefined(string Kod)
        {
            var result = GetScalarQuery("select Count(*) from Kisim where Kod= @Kod and Silindi=0", new { Kod }) + "";
            int.TryParse(result, out int count);
            return count > 0;
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_KisimDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<ColumnNameTemp> GetColumnNames(string tableName)
        {
            return new DpDtoRepositoryBase<ColumnNameTemp>().GetListDtoQuery($@"SELECT COLUMN_NAME
            FROM INFORMATION_SCHEMA.COLUMNS
            WHERE TABLE_NAME = @tableName and COLUMN_NAME != 'Silindi' and COLUMN_NAME NOT IN(
                SELECT COLUMN_NAME
            FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                WHERE OBJECTPROPERTY(OBJECT_ID(CONSTRAINT_SCHEMA + '.' + QUOTENAME(CONSTRAINT_NAME)), 'IsPrimaryKey') = 1
            AND TABLE_NAME = @tableName)", new { tableName });
        }

        public List<string> AddListWithTransactionBySablon(List<Kisim> listKisim)
        {
            List<string> listKisimID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }


                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var kisim in listKisim)
                    {
                        var strKisimID = connection.ExecuteScalar("insert into Kisim(Kod,Ad,Butce,HedeflenenButce,VardiyaSinifID,SarfYeriID,Aciklama) values (@Kod,@Ad,@Butce,@HedeflenenButce,@VardiyaSinifID,@SarfYeriID,@Aciklama); " +
                            "SELECT CAST(SCOPE_IDENTITY() as int)", kisim, transaction);


                        listKisimID.Add(strKisimID + "");
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }

            }
            return listKisimID;
        }


    }
}