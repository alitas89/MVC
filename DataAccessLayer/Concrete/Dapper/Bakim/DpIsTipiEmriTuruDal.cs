using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpIsTipiEmirTuruDal : DpEntityRepositoryBase<IsTipiEmirTuru>, IIsTipiEmirTuruDal
    {
        public List<IsTipiEmirTuru> GetList()
        {
            return GetListQuery("select * from IsTipiEmirTuru where Silindi=0", new { });
        }

        public List<IsTipiEmirTuruDto> GetList(int IsTipiID)
        {
            return new DpDtoRepositoryBase<IsTipiEmirTuruDto>().GetListDtoQuery("select * from View_IsTipiEmirTuruDto where IsTipiID= @IsTipiID and Silindi=0", new { IsTipiID });

        }

        public IsTipiEmirTuru Get(int Id)
        {
            return GetQuery("select * from IsTipiEmirTuru where IsTipiEmirTuruID= @Id and Silindi=0", new { Id });
        }

        public int Add(IsTipiEmirTuru isTipiEmirTuru)
        {
            return AddQuery("insert into IsTipiEmirTuru(IsTipiID,IsEmriTuruID,Silindi) values (@IsTipiID,@IsEmriTuruID,@Silindi)", isTipiEmirTuru);
        }

        public int Update(IsTipiEmirTuru isTipiEmirTuru)
        {
            return UpdateQuery("update IsTipiEmirTuru set IsTipiID=@IsTipiID,IsEmriTuruID=@IsEmriTuruID,Silindi=@Silindi where IsTipiEmirTuruID=@IsTipiEmirTuruID", isTipiEmirTuru);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from IsTipiEmirTuru where IsTipiEmirTuruID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update IsTipiEmirTuru set Silindi = 1 where IsTipiEmirTuruID=@Id", new { Id });
        }

        public List<IsTipiEmirTuru> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM IsTipiEmirTuru where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM IsTipiEmirTuru where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public List<IsTipiEmirTuruDto> GetListPaginationDto(int IsTipiID, PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<IsTipiEmirTuruDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_IsTipiEmirTuruDto where Silindi=0 and IsTipiID=@IsTipiID {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { IsTipiID, pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(int IsTipiID, string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_IsTipiEmirTuruDto where Silindi=0 and ZimmetTransferID=@ZimmetTransferID {filterQuery} ", new { IsTipiID }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public int AddWithTransaction(int IsTipiID, List<int> listIsTipiEmirTuru)
        {
            var count = 0;

            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                connection.Execute("update IsTipiEmirTuru set Silindi = 1 where IsTipiID=@IsTipiID", new { IsTipiID }, transaction);

                foreach (var IsEmriTuruID in listIsTipiEmirTuru)
                {
                   
                    count += connection.Execute("insert into IsTipiEmirTuru(IsTipiID,IsEmriTuruID, Silindi) values (@IsTipiID,@IsEmriTuruID,@Silindi)", 
                        new IsTipiEmirTuru(){ IsTipiID = IsTipiID, IsEmriTuruID = IsEmriTuruID, Silindi = false}, transaction);
                }

                transaction.Commit();
            }
            return count;
        }

        public int UpdateWithTransaction(int IsTipiID, List<int> listIsTipiEmirTuru)
        {
            var count = 0;
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                foreach (var IsEmriTuruID in listIsTipiEmirTuru)
                {
                    IsTipiEmirTuru isTipiEmirTuru = new IsTipiEmirTuru()
                    {
                        IsTipiID = IsTipiID,
                        Silindi = false,
                        IsEmriTuruID = IsEmriTuruID,
                        
                    };

                    //if (DurumID == 1)
                    //{
                    //    count += connection.Execute("insert into IsTipiEmirTuru(IsTipiID,IsEmriTuruID) values (@IsTipiID,@IsEmriTuruID)", item, transaction);
                    //}
                    //else if (item.DurumID == 2)
                    //{
                    //    count += connection.Execute("update IsTipiEmirTuru set IsTipiID=@IsTipiID,IsEmriTuruID=@IsEmriTuruID where IsTipiEmirTuruID=@IsTipiEmirTuruID", item, transaction);
                    //}
                    //else if (item.DurumID == 3)
                    //{
                    //    count += connection.Execute("update IsTipiEmirTuru set Silindi = 1 where IsTipiEmirTuruID=@IsTipiEmirTuruID", item, transaction);
                    //}
                }

                transaction.Commit();
            }
            return count;
        }       
    }
}
