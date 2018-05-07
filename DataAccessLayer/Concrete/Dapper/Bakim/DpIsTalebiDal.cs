using System;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Core.Utilities.Dal;
using Dapper;
using EntityLayer.ComplexTypes.DtoModel.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpIsTalebiDal : DpEntityRepositoryBase<IsTalebi>, IIsTalebiDal
    {
        public List<IsTalebi> GetList()
        {
            return GetListQuery("select * from IsTalebi where Silindi=0", new { });
        }

        public IsTalebi Get(int Id)
        {
            return GetQuery("select * from IsTalebi where IsTalebiID= @Id and Silindi=0", new { Id });
        }

        public int Add(IsTalebi ıstalebi)
        {
            return AddQuery("insert into IsTalebi(TalepYil,IsEmriTuruID,BakimOncelikID,VarlikID,KisimID,ArizaOlusmaTarih,ArizaOlusmaSaat,BildirilisTarih,BildirilisSaat,TalepEdenID,IsTipiID,BakimArizaID,Aciklama,OnaylayanID,OnaylayanAciklama,SorumluID,EkipID,OnayTarih,OnaySaat,StatuID,Silindi) values (@TalepYil,@IsEmriTuruID,@BakimOncelikID,@VarlikID,@KisimID,@ArizaOlusmaTarih,@ArizaOlusmaSaat,@BildirilisTarih,@BildirilisSaat,@TalepEdenID,@IsTipiID,@BakimArizaID,@Aciklama,@OnaylayanID,@OnaylayanAciklama,@SorumluID,@EkipID,@OnayTarih,@OnaySaat,@StatuID,@Silindi);" +
                " SELECT CAST(SCOPE_IDENTITY() as int)", ıstalebi, true);
        }

        public int Update(IsTalebi ıstalebi)
        {
            return UpdateQuery("update IsTalebi set TalepYil=@TalepYil,IsEmriTuruID=@IsEmriTuruID,BakimOncelikID=@BakimOncelikID,VarlikID=@VarlikID,KisimID=@KisimID,ArizaOlusmaTarih=@ArizaOlusmaTarih,ArizaOlusmaSaat=@ArizaOlusmaSaat,BildirilisTarih=@BildirilisTarih,BildirilisSaat=@BildirilisSaat,TalepEdenID=@TalepEdenID,IsTipiID=@IsTipiID,BakimArizaID=@BakimArizaID,Aciklama=@Aciklama,OnaylayanID=@OnaylayanID,OnaylayanAciklama=@OnaylayanAciklama,SorumluID=@SorumluID,EkipID=@EkipID,OnayTarih=@OnayTarih,OnaySaat=@OnaySaat,StatuID=@StatuID,Silindi=@Silindi where IsTalebiID=@IsTalebiID", ıstalebi);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from IsTalebi where IsTalebiID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update IsTalebi set Silindi = 1 where IsTalebiID=@Id", new { Id });
        }

        public List<IsTalebi> GetListPagination(PagingParams pagingParams)
        {
            string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            return GetListQuery($@"SELECT * FROM IsTalebi where Silindi=0 {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM IsTalebi where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<IsTalebiDto> GetListPaginationDto(PagingParams pagingParams)
        {
            string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            return new DpDtoRepositoryBase<IsTalebiDto>().GetListDtoQuery($@"SELECT * FROM View_IsTalebiDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public List<IsTalebiDto> GetListPaginationDtoKullaniciID(PagingParams pagingParams, int KullaniciID)
        {
            string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            //Giriş yapmış olan kullanıcıya göre filtreleyerek getir. (Kendi İş Talepleri)
            //İş emircisi kendi iş tipindeki iş taleplerini görecek.
            return new DpDtoRepositoryBase<IsTalebiDto>().GetListDtoQuery($@"
                SELECT * FROM View_IsTalebiDto where Silindi=0 and (TalepEdenID=@KullaniciID or 
                IsTipiID IN (select IsTipiID from IsTalebiOnayBirim where KullaniciID=@KullaniciID and Silindi=0))
                {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { KullaniciID, pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int AddWithTransaction(IsTalebi ıstalebi)
        {
            int IsEmriNoID = 0;
            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MvcContext"].ConnectionString))
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IDbTransaction transaction = connection.BeginTransaction();

                var strIsTalepID = connection.ExecuteScalar("insert into IsTalebi(TalepYil,IsEmriTuruID,BakimOncelikID,VarlikID,KisimID,ArizaOlusmaTarih,ArizaOlusmaSaat,BildirilisTarih,BildirilisSaat,TalepEdenID,IsTipiID,BakimArizaID,Aciklama,OnaylayanID,OnaylayanAciklama,SorumluID,EkipID,OnayTarih,OnaySaat,StatuID,Silindi) values (@TalepYil,@IsEmriTuruID,@BakimOncelikID,@VarlikID,@KisimID,@ArizaOlusmaTarih,@ArizaOlusmaSaat,@BildirilisTarih,@BildirilisSaat,@TalepEdenID,@IsTipiID,@BakimArizaID,@Aciklama,@OnaylayanID,@OnaylayanAciklama,@SorumluID,@EkipID,@OnayTarih,@OnaySaat,@StatuID,@Silindi); " +
                                   "SELECT CAST(SCOPE_IDENTITY() as int)", ıstalebi, transaction);
                int.TryParse(strIsTalepID + "", out int IsTalepID);

                var strIsEmriNoID = connection.ExecuteScalar(
                        "insert into IsEmriNo(IsTalepID,IsEmriID,Tarih,Silindi) values (@IsTalepID,null,GetDate(),0)" +
                             "SELECT CAST(SCOPE_IDENTITY() as int)",
                        new { IsTalepID }, transaction);
                int.TryParse(strIsEmriNoID + "", out IsEmriNoID);

                transaction.Commit();
            }
            return IsEmriNoID;
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_IsTalebiDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciID(int KullaniciID)
        {
            return new DpDtoRepositoryBase<IsTipiForKullaniciTemp>().GetListDtoQuery($@"select a.IsTipiID, b.Ad as IsTipiAd, b.Kod from IsTalebiBirim a inner join IsTipi b on a.IsTipiID=b.IsTipiID where a.KullaniciID=@KullaniciID and a.Silindi=0",
                new { KullaniciID });
        }

        public List<IsTipiForKullaniciTemp> GetIsEmriListByKullaniciID(int KullaniciID)
        {
            return new DpDtoRepositoryBase<IsTipiForKullaniciTemp>().GetListDtoQuery($@"select a.IsTipiID, b.Ad as IsTipiAd, b.Kod from IsTalebiOnayBirim a inner join IsTipi b on a.IsTipiID=b.IsTipiID where a.KullaniciID=@KullaniciID and a.Silindi=0",
                new { KullaniciID });
        }

        public List<EmirTuruIsTipiTemp> GetEmirTuruListByIsTipiID(int IsTipiID)
        {
            return new DpDtoRepositoryBase<EmirTuruIsTipiTemp>().GetListDtoQuery($@"select a.IsEmriTuruID, b.Ad from IsTipiEmirTuru a inner join IsEmriTuru b on a.IsEmriTuruID = b.IsEmriTuruID where a.Silindi=0 and b.Silindi=0 and a.IsTipiID=@IsTipiID",
                new { IsTipiID });
        }

        public List<IsEmriNo> GetIsEmriNoByIsTalepID(int IsTalepID)
        {
            var list = new DpDtoRepositoryBase<IsEmriNo>().GetListDtoQuery("select * from IsEmriNo where IsTalepID= @IsTalepID and Silindi=0", new { IsTalepID });
            return list;
        }
    }
}
