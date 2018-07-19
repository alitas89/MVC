using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;
using Core.Utilities.Dal;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpAkaryakitAlimFisDal : DpEntityRepositoryBase<AkaryakitAlimFis>, IAkaryakitAlimFisDal
    {
        public List<AkaryakitAlimFis> GetList()
        {
            return GetListQuery("select * from AkaryakitAlimFis where Silindi=0", new { });
        }

        public AkaryakitAlimFis Get(int Id)
        {
            return GetQuery("select * from AkaryakitAlimFis where AkaryakitAlimFisID= @Id and Silindi=0", new { Id });
        }

        public int Add(AkaryakitAlimFis akaryakitalimfis)
        {
            return AddQuery("insert into AkaryakitAlimFis(FisNo,AracID,YakitID,AmbarID,Miktar,BirimFiyat,Iskonto,ToplamAkaryakitTutari,MasrafYeriID,YakitAlanKisiID,SaticiID,YakitAlimTarih,YakitAlimSaat,AracKm,Aciklama,Silindi) values (@FisNo,@AracID,@YakitID,@AmbarID,@Miktar,@BirimFiyat,@Iskonto,@ToplamAkaryakitTutari,@MasrafYeriID,@YakitAlanKisiID,@SaticiID,@YakitAlimTarih,@YakitAlimSaat,@AracKm,@Aciklama,@Silindi)", akaryakitalimfis);
        }

        public int Update(AkaryakitAlimFis akaryakitalimfis)
        {
            return UpdateQuery("update AkaryakitAlimFis set FisNo=@FisNo,AracID=@AracID,YakitID=@YakitID,AmbarID=@AmbarID,Miktar=@Miktar,BirimFiyat=@BirimFiyat,Iskonto=@Iskonto,ToplamAkaryakitTutari=@ToplamAkaryakitTutari,MasrafYeriID=@MasrafYeriID,YakitAlanKisiID=@YakitAlanKisiID,SaticiID=@SaticiID,YakitAlimTarih=@YakitAlimTarih,YakitAlimSaat=@YakitAlimSaat,AracKm=@AracKm,Aciklama=@Aciklama,Silindi=@Silindi where AkaryakitAlimFisID=@AkaryakitAlimFisID", akaryakitalimfis);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from AkaryakitAlimFis where AkaryakitAlimFisID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update AkaryakitAlimFis set Silindi = 1 where AkaryakitAlimFisID=@Id", new { Id });
        }

        public List<AkaryakitAlimFis> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM AkaryakitAlimFis where Silindi=0 {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }


        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM AkaryakitAlimFis  where Silindi=0 {filterQuery}", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<AkaryakitAlimFisDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<AkaryakitAlimFisDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_AkaryakitAlimFisDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_AkaryakitAlimFisDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<AkaryakitAlimFis> listAkaryakitAlimFis)
        {
            List<string> listAkaryakitAlimFisID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var akaryakitalimfis in listAkaryakitAlimFis)
                    {
                        var strAkaryakitAlimFisID = connection.ExecuteScalar("insert into AkaryakitAlimFis(FisNo,AracID,YakitID,AmbarID,Miktar,BirimFiyat,Iskonto,ToplamAkaryakitTutari,MasrafYeriID,YakitAlanKisiID,SaticiID,YakitAlimTarih,YakitAlimSaat,AracKm,Aciklama) values (@FisNo,@AracID,@YakitID,@AmbarID,@Miktar,@BirimFiyat,@Iskonto,@ToplamAkaryakitTutari,@MasrafYeriID,@YakitAlanKisiID,@SaticiID,@YakitAlimTarih,@YakitAlimSaat,@AracKm,@Aciklama);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", akaryakitalimfis, transaction);

                        listAkaryakitAlimFisID.Add(strAkaryakitAlimFisID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listAkaryakitAlimFisID;
            }
        }


    }
}

