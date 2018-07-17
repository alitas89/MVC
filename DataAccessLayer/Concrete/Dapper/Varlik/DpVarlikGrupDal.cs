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
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpVarlikGrupDal : DpEntityRepositoryBase<VarlikGrup>, IVarlikGrupDal
    {
        public List<VarlikGrup> GetList()
        {
            return GetListQuery($"select * from VarlikGrup where Silindi=0", new { });
        }

        public VarlikGrup Get(int Id)
        {
            return GetQuery("select * from VarlikGrup where VarlikGrupID= @Id and Silindi=0", new { Id });
        }

        public int Add(VarlikGrup varlikgrup)
        {
            return AddQuery("insert into VarlikGrup(Kod,Ad,IsTipiID,Aciklama1,Aciklama2,Aciklama3) values (@Kod,@Ad,@IsTipiID,@Aciklama1,@Aciklama2,@Aciklama3)", varlikgrup);
        }

        public int Update(VarlikGrup varlikgrup)
        {
            return UpdateQuery("update VarlikGrup set Kod=@Kod,Ad=@Ad,IsTipiID=@IsTipiID,Aciklama1=@Aciklama1,Aciklama2=@Aciklama2,Aciklama3=@Aciklama3 where VarlikGrupID=@VarlikGrupID", varlikgrup);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from VarlikGrup where VarlikGrupID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update VarlikGrup set Silindi = 1 where VarlikGrupID=@Id", new { Id });
        }

        public List<VarlikGrupDto> GetListDto()
        {
            return new DpDtoRepositoryBase<VarlikGrupDto>().GetListDtoQuery("select VG.*, IT.Ad as IsTipiAd from VarlikGrup VG left join IsTipi IT on IT.IsTipiID = VG.IsTipiID", new { });
        }

        public List<VarlikGrup> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM VarlikGrup where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM VarlikGrup where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<VarlikGrupDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<VarlikGrupDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_VarlikGrupDto where Silindi=0 {filterQuery} {orderQuery}
            OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_VarlikGrupDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }


        public List<string> AddListWithTransactionBySablon(List<VarlikGrup> listVarlikGrup)
        {
            List<string> listVarlikGrupID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var varlikgrup in listVarlikGrup)
                    {
                        var strVarlikGrupID = connection.ExecuteScalar("insert into VarlikGrup(Kod,Ad,IsTipiID,Aciklama1,Aciklama2,Aciklama3) values (@Kod,@Ad,@IsTipiID,@Aciklama1,@Aciklama2,@Aciklama3);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", varlikgrup, transaction);

                        listVarlikGrupID.Add(strVarlikGrupID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listVarlikGrupID;
            }

        }
    }
}