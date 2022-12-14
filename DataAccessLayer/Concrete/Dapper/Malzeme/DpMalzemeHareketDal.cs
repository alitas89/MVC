using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace DataAccessLayer.Concrete.Dapper.Malzeme
{
    public class DpMalzemeHareketDal : DpEntityRepositoryBase<MalzemeHareket>, IMalzemeHareketDal
    {
        public List<MalzemeHareket> GetList()
        {
            return GetListQuery("select * from MalzemeHareket where Silindi=0", new { });
        }

        public MalzemeHareket Get(int Id)
        {
            return GetQuery("select * from MalzemeHareket where MalzemeHareketID= @Id and Silindi=0", new { Id });
        }

        public int Add(MalzemeHareket malzemehareket)
        {
            return AddQuery("insert into MalzemeHareket(MalzemeHareketFisNo,AmbarID,AmbarID,Aciklama,MalzemeHareketTuruID,Silindi) values (@MalzemeHareketFisNo,@AmbarID,@Ambar2ID,@Aciklama,@MalzemeHareketTuruID,@Silindi)", malzemehareket);
        }

        public int Update(MalzemeHareket malzemehareket)
        {
            return UpdateQuery("update MalzemeHareket set MalzemeHareketFisNo=@MalzemeHareketFisNo,AmbarID=@AmbarID,Ambar2ID=@Ambar2ID,Aciklama=@Aciklama,MalzemeHareketTuruID=@MalzemeHareketTuruID,Silindi=@Silindi where MalzemeHareketID=@MalzemeHareketID", malzemehareket);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from MalzemeHareket where MalzemeHareketID=@Id ", new { Id });
        }

        //Id olarak MalzemeHareketFisNo kullanılmaktadır.
        public int DeleteSoft(int Id)
        {   
            var count = 0;
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                connection.Execute("update MalzemeHareket set Silindi = 1 where MalzemeHareketFisNo=@Id", Id, transaction);

                connection.Execute("update MalzemeHareketDetay set Silindi = 1 where MalzemeHareketFisNo=@Id", Id, transaction);

                transaction.Commit();
            }
            return count;
        }

        public List<MalzemeHareket> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM MalzemeHareket where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM MalzemeHareket where Silindi = 0 { filterQuery}", new { }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public List<MalzemeHareketDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<MalzemeHareketDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_MalzemeHareketDto where Silindi=0 {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_MalzemeHareketDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public int AddWithTransaction(MalzemeHareketTemp malzemeHareketTemp, List<MalzemeHareketDetay> listMalzeme)
        {
            var count = 0;
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                connection.Execute("update MalzemeHareketFis set FisTarih=@FisTarih,FisSaat=@FisSaat where MalzemeHareketFisNo=@MalzemeHareketFisNo", malzemeHareketTemp, transaction);

                if (malzemeHareketTemp.MalzemeHareketTuruID == 3)
                {
                    connection.Execute("insert into MalzemeHareket(MalzemeHareketFisNo,AmbarID,Ambar2ID,MalzemeHareketTuruID,IsTransfer) values (@MalzemeHareketFisNo,@AmbarID,@Ambar2ID,@MalzemeHareketTuruID,1)", malzemeHareketTemp, transaction);
                    connection.Execute("insert into MalzemeHareket(MalzemeHareketFisNo,AmbarID,MalzemeHareketTuruID,IsTransfer) values (@MalzemeHareketFisNo,@AmbarID,1,1)", malzemeHareketTemp, transaction);
                    connection.Execute("insert into MalzemeHareket(MalzemeHareketFisNo,AmbarID,MalzemeHareketTuruID,IsTransfer) values (@MalzemeHareketFisNo,@Ambar2ID,2,1)", malzemeHareketTemp, transaction);
                }
                else
                {
                    connection.Execute("insert into MalzemeHareket(MalzemeHareketFisNo,AmbarID,Ambar2ID,Aciklama,MalzemeHareketTuruID) values (@MalzemeHareketFisNo,@AmbarID,@Ambar2ID,@Aciklama,@MalzemeHareketTuruID)", malzemeHareketTemp, transaction);
                }

                foreach (var malzeme in listMalzeme)
                {
                    malzeme.MalzemeHareketFisNo = malzemeHareketTemp.MalzemeHareketFisNo;
                    count += connection.Execute("insert into MalzemeHareketDetay(MalzemeHareketFisNo,MalzemeID,Miktar) values (@MalzemeHareketFisNo,@MalzemeID,@Miktar)", malzeme, transaction);
                }

                transaction.Commit();
            }
            return count;
        }

        public int UpdateWithTransaction(MalzemeHareketTemp malzemeHareketTemp, List<MalzemeHareketDetay> listMalzeme)
        {
            var count = 0;
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                connection.Execute("update MalzemeHareketFis set FisTarih=@FisTarih,FisSaat=@FisSaat where MalzemeHareketFisNo=@MalzemeHareketFisNo", malzemeHareketTemp, transaction);

                connection.Execute("update MalzemeHareket set AmbarID=@AmbarID,Ambar2ID=@Ambar2ID,Aciklama=@Aciklama where MalzemeHareketID=@MalzemeHareketID", malzemeHareketTemp, transaction);


                foreach (var malzeme in listMalzeme)
                {
                    if (malzeme.DurumID == 1)
                    {
                        count += connection.Execute("insert into MalzemeHareketDetay(MalzemeHareketFisNo,MalzemeID,Miktar) values (@MalzemeHareketFisNo,@MalzemeID,@Miktar)",
                           new MalzemeHareketDetay()
                           {
                               DurumID = 1,
                               MalzemeHareketFisNo = malzemeHareketTemp.MalzemeHareketFisNo,
                               MalzemeID = malzeme.MalzemeID,
                               Miktar = malzeme.Miktar,
                               Silindi = false
                           }, transaction);
                    }
                    else if (malzeme.DurumID == 2)
                    {
                        count += connection.Execute("update MalzemeHareketDetay set Silindi = 1 where MalzemeHareketDetayID=@MalzemeHareketDetayID", malzeme, transaction);
                    }
                    else if (malzeme.DurumID == 3)
                    {
                        count += connection.Execute("update MalzemeHareketDetay set MalzemeID=@MalzemeID,Miktar=@Miktar where MalzemeHareketDetayID=@MalzemeHareketDetayID", malzeme, transaction);
                    }
                }

                transaction.Commit();
            }
            return count;
        }
    }
}
