using Core.DataAccessLayer.Dapper;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using System.Collections.Generic;
using Core.Utilities.Dal;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.Concrete.Varlik;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpVarlikDal : DpEntityRepositoryBase<EntityLayer.Concrete.Varlik.Varlik>, IVarlikDal
    {
        public List<EntityLayer.Concrete.Varlik.Varlik> GetList()
        {
            return GetListQuery("select * from Varlik where Silindi=0", new { });
        }

        public List<EntityLayer.Concrete.Varlik.Varlik> GetListByKisimID(int KisimID)
        {
            return GetListQuery($"select * from Varlik where Silindi=0 and KisimID=@KisimID", new { KisimID });
        }

        public List<EntityLayer.Concrete.Varlik.Varlik> GetListByKaynakID(int KaynakID)
        {
            return GetListQuery($"select * from Varlik where Silindi=0 and ZimmetliPersonelID=@KaynakID", new { KaynakID });
        }

        public EntityLayer.Concrete.Varlik.Varlik Get(int Id)
        {
            return GetQuery("select * from Varlik where VarlikID= @Id and Silindi=0", new { Id });
        }

        public int Add(EntityLayer.Concrete.Varlik.Varlik varlik)
        {
            return AddQuery("insert into Varlik(Kod,Ad,VarlikDurumID,VarlikTuruID,VarlikGrupID,BagliVarlikKod,KisimID,SarfYeriID,IsletmeID,MarkaID,ModelID,SeriNo,BarkodKod,GarantiBitisTarih,SonKullanimTarih,Aciklama,ZimmetliPersonelID,VarsayilanIsTipiID,VarsayilanBakimArizaID,VarsayilanArizaNedenID,VarsayilanArizaCozumID,EkipID,IsEmriTurID,Silindi) values (@Kod,@Ad,@VarlikDurumID,@VarlikTuruID,@VarlikGrupID,@BagliVarlikKod,@KisimID,@SarfYeriID,@IsletmeID,@MarkaID,@ModelID,@SeriNo,@BarkodKod,@GarantiBitisTarih,@SonKullanimTarih,@Aciklama,@ZimmetliPersonelID,@VarsayilanIsTipiID,@VarsayilanBakimArizaID,@VarsayilanArizaNedenID,@VarsayilanArizaCozumID,@EkipID,@IsEmriTurID,@Silindi); " +
                " SELECT CAST(SCOPE_IDENTITY() as int)", varlik, true);
        }

        public int Update(EntityLayer.Concrete.Varlik.Varlik varlik)
        {
            return UpdateQuery("update Varlik set Kod=@Kod,Ad=@Ad,VarlikDurumID=@VarlikDurumID,VarlikTuruID=@VarlikTuruID,VarlikGrupID=@VarlikGrupID,BagliVarlikKod=@BagliVarlikKod,KisimID=@KisimID,SarfYeriID=@SarfYeriID,IsletmeID=@IsletmeID,MarkaID=@MarkaID,ModelID=@ModelID,SeriNo=@SeriNo,BarkodKod=@BarkodKod,GarantiBitisTarih=@GarantiBitisTarih,SonKullanimTarih=@SonKullanimTarih,Aciklama=@Aciklama,ZimmetliPersonelID=@ZimmetliPersonelID,VarsayilanIsTipiID=@VarsayilanIsTipiID,VarsayilanBakimArizaID=@VarsayilanBakimArizaID,VarsayilanArizaNedenID=@VarsayilanArizaNedenID,VarsayilanArizaCozumID=@VarsayilanArizaCozumID,EkipID=@EkipID,IsEmriTurID=@IsEmriTurID,Silindi=@Silindi where VarlikID=@VarlikID", varlik);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Varlik where VarlikID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Varlik set Silindi = 1 where VarlikID=@Id", new { Id });
        }

        public List<EntityLayer.Concrete.Varlik.Varlik> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT {columnsQuery} FROM Varlik where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }


        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Varlik where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<VarlikDto> GetListDto()
        {
            return new DpDtoRepositoryBase<VarlikDto>().GetListDtoQuery("SELECT * FROM View_VarlikDto where Silindi=0", new { });
        }

        public List<VarlikDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<VarlikDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_VarlikDto where Silindi=0 {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_VarlikDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public bool IsKodDefined(string Kod)
        {
            var result = GetScalarQuery("select Count(*) from Varlik where Kod= @Kod and Silindi=0", new { Kod }) + "";
            int.TryParse(result, out int count);
            return count > 0;
        }

        public List<VarlikDto> GetListPaginationDtoByVarlikGrupID(int VarlikGrupID, PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<VarlikDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_VarlikDto where Silindi=0 and VarlikGrupID=@VarlikGrupID {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit, VarlikGrupID });
        }

        public int GetCountDtoByVarlikGrupID(int VarlikGrupID, string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_VarlikDto where Silindi=0 and VarlikGrupID= @VarlikGrupID {filterQuery} ", new { VarlikGrupID }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<string> AddListWithTransactionBySablon(List<EntityLayer.Concrete.Varlik.Varlik> listVarlik)
        {
            List<string> listVarlikID = new List<string>();
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                try
                {
                    IDbTransaction transaction = connection.BeginTransaction();
                    foreach (var varlik in listVarlik)
                    {
                        var strVarlikID = connection.ExecuteScalar("insert into Varlik(Kod,Ad,VarlikDurumID,VarlikTuruID,VarlikGrupID,BagliVarlikKod,KisimID,SarfYeriID,IsletmeID,MarkaID,ModelID,SeriNo,BarkodKod,GarantiBitisTarih,SonKullanimTarih,Aciklama,ZimmetliPersonelID,VarsayilanIsTipiID,VarsayilanBakimArizaID,VarsayilanArizaNedenID,VarsayilanArizaCozumID,EkipID,IsEmriTurID) values (@Kod,@Ad,@VarlikDurumID,@VarlikTuruID,@VarlikGrupID,@BagliVarlikKod,@KisimID,@SarfYeriID,@IsletmeID,@MarkaID,@ModelID,@SeriNo,@BarkodKod,@GarantiBitisTarih,@SonKullanimTarih,@Aciklama,@ZimmetliPersonelID,@VarsayilanIsTipiID,@VarsayilanBakimArizaID,@VarsayilanArizaNedenID,@VarsayilanArizaCozumID,@EkipID,@IsEmriTurID);" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)", varlik, transaction);

                        listVarlikID.Add(strVarlikID + "");
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    return new List<string>() { "0" };
                }
                return listVarlikID;
            }
        }

    }
}