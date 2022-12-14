using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpZimmetTransferDetayDal : DpEntityRepositoryBase<ZimmetTransferDetay>, IZimmetTransferDetayDal
    {
        public List<ZimmetTransferDetay> GetList()
        {
            return GetListQuery("select * from ZimmetTransferDetay where Silindi=0", new { });
        }

        public ZimmetTransferDetay Get(int Id)
        {
            return GetQuery("select * from ZimmetTransferDetay where ZimmetTransferDetayID= @Id and Silindi=0", new { Id });
        }

        public int Add(ZimmetTransferDetay zimmettransferdetay)
        {
            return AddQuery("insert into ZimmetTransferDetay(VarlikID,ZimmetTransferID,Silindi) values (@VarlikID,@ZimmetTransferID,@Silindi)", zimmettransferdetay);
        }

        public int Update(ZimmetTransferDetay zimmettransferdetay)
        {
            return UpdateQuery("update ZimmetTransferDetay set VarlikID=@VarlikID,ZimmetTransferID=@ZimmetTransferID,Silindi=@Silindi where ZimmetTransferDetayID=@ZimmetTransferDetayID", zimmettransferdetay);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from ZimmetTransferDetay where ZimmetTransferDetayID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update ZimmetTransferDetay set Silindi = 1 where ZimmetTransferDetayID=@Id", new { Id });
        }

        public List<ZimmetTransferDetay> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM ZimmetTransferDetay where Silindi=0 {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM ZimmetTransferDetay where Silindi=0 {filterQuery}", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<ZimmetTransferDetayDto> GetList(int ZimmetTransferID)
        {
            return new DpDtoRepositoryBase<ZimmetTransferDetayDto>().GetListDtoQuery("select * from View_ZimmetTransferDetayDto where ZimmetTransferID= @ZimmetTransferID and Silindi=0", new { ZimmetTransferID });
        }

        public List<ZimmetTransferDetayDto> GetListPaginationDto(int ZimmetTransferID, PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<ZimmetTransferDetayDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_ZimmetTransferDetayDto where Silindi=0 and ZimmetTransferID=@ZimmetTransferID {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { ZimmetTransferID, pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(int ZimmetTransferID, string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_ZimmetTransferDetayDto where Silindi=0 and ZimmetTransferID=@ZimmetTransferID {filterQuery} ", new { ZimmetTransferID }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public int GetZimmetliPersonel(int ZimmetTransferID)
        {
            var zimmetAlan = GetScalarQuery("select ZimmetAlanID from ZimmetTransfer where ZimmetTransferID=@ZimmetTransferID", new { ZimmetTransferID }) + "";
            int.TryParse(zimmetAlan, out int zimmetAlanID);
            return zimmetAlanID;
        }

        public int UpdateVarlikZimmet(int VarlikID, int ZimmetliPersonelID)
        {
            return UpdateQuery("update Varlik set ZimmetliPersonelID=@ZimmetliPersonelID where VarlikID=@VarlikID", new { ZimmetliPersonelID, VarlikID });
        }

        public int AddWithTransaction(int ZimmetTransferID, List<ZimmetTransferDetay> listZimmetTransferDetay)
        {
            var count = 0;

            //Kişi bilgisi alınır
            int zimmetliPersonelID = GetZimmetliPersonel(ZimmetTransferID);

            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                foreach (var item in listZimmetTransferDetay)
                {
                    item.ZimmetTransferID = ZimmetTransferID;

                    connection.Execute("update Varlik set ZimmetliPersonelID=@ZimmetliPersonelID where VarlikID=@VarlikID", new { zimmetliPersonelID, item.VarlikID }, transaction);

                    count += connection.Execute("insert into ZimmetTransferDetay(VarlikID,ZimmetTransferID,Silindi) values (@VarlikID,@ZimmetTransferID,@Silindi)", item, transaction);
                }

                transaction.Commit();
            }
            return count;
        }

        public int UpdateWithTransaction(int ZimmetTransferID, List<ZimmetTransferDetayDto> listZimmetTransferDetay)
        {
            var count = 0;

            //Kişi bilgisi alınır
            int zimmetliPersonelID = GetZimmetliPersonel(ZimmetTransferID);

            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                foreach (var zimmetTransferDetay in listZimmetTransferDetay)
                {
                    zimmetTransferDetay.ZimmetTransferID = ZimmetTransferID;

                    switch (zimmetTransferDetay.DurumID)
                    {
                        case 0:
                            break;
                        case 1:
                            connection.Execute("update Varlik set ZimmetliPersonelID=@ZimmetliPersonelID where VarlikID=@VarlikID", new { zimmetliPersonelID, zimmetTransferDetay.VarlikID }, transaction);
                            count += connection.Execute("insert into ZimmetTransferDetay(VarlikID,ZimmetTransferID,Silindi) values (@VarlikID,@ZimmetTransferID,@Silindi)", zimmetTransferDetay, transaction);
                            break;
                        case 2:
                            count += connection.Execute("update ZimmetTransferDetay set Silindi = 1 where ZimmetTransferDetayID=@ZimmetTransferDetayID", zimmetTransferDetay, transaction);
                            break;
                        case 3:
                            connection.Execute("update Varlik set ZimmetliPersonelID=@ZimmetliPersonelID where VarlikID=@VarlikID", new { zimmetliPersonelID, zimmetTransferDetay.VarlikID }, transaction);
                            count += connection.Execute("update ZimmetTransferDetay set VarlikID=@VarlikID,ZimmetTransferID=@ZimmetTransferID,Silindi=@Silindi where ZimmetTransferDetayID=@ZimmetTransferDetayID", zimmetTransferDetay, transaction);
                            break;
                        default:
                            break;
                    }
                }

                transaction.Commit();
            }
            return count;
        }
    }
}
