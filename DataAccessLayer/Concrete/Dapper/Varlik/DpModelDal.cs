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
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpModelDal : DpEntityRepositoryBase<Model>, IModelDal
    {
        public List<Model> GetList()
        {
            return GetListQuery($"select * from Model where Silindi=0", new { });
        }

        public Model Get(int Id)
        {
            return GetQuery("select * from Model where ModelID= @Id and Silindi=0", new { Id });
        }

        public int Add(Model model)
        {
            return AddQuery("insert into Model(Kod,Ad,Aciklama,MarkaID) values (@Kod,@Ad,@Aciklama,@MarkaID)", model);
        }

        public int Update(Model model)
        {
            return UpdateQuery("update Model set Kod=@Kod,Ad=@Ad,Aciklama=@Aciklama,MarkaID=@MarkaID where ModelID=@ModelID", model);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Model where ModelID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Model set Silindi = 1 where ModelID=@Id", new { Id });
        }

        public List<ModelDto> GetListDto()
        {
            return new DpDtoRepositoryBase<ModelDto>().GetListDtoQuery("select M.*, MA.Ad as MarkaAd from Model M left join Marka MA on MA.MarkaID = M.MarkaID", new { });
        }
        public List<Model> GetListPagination(PagingParams pagingParams)
        {
              string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            return GetListQuery($@"SELECT * FROM Model where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Model where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<ModelDto> GetListPaginationDto(PagingParams pagingParams)
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
            
            return new DpDtoRepositoryBase<ModelDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_ModelDto where Silindi=0 {filterQuery} {orderQuery}
                                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_ModelDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<Model> listModel)
        {
            List<string> listModelID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var model in listModel)
                    {
                        var strModelID = connection.ExecuteScalar("insert into Model(Kod,Ad,MarkaID,Aciklama) values (@Kod,@Ad,@MarkaID,@Aciklama);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", model, transaction);

                        listModelID.Add(strModelID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listModelID;
            }

        }
    }
}