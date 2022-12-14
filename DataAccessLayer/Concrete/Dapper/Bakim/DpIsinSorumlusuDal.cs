using System.Collections.Generic;
using System.Linq;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpIsinSorumlusuDal : DpEntityRepositoryBase<IsEmri>, IIsinSorumlusuDal
    {
        #region KullanılanAlanlar
        public IsEmriDto GetByKullaniciID(int IsEmriID, int KullaniciID)
        {
            var list = new DpDtoRepositoryBase<IsEmriDto>().GetListDtoQuery("select * from View_IsEmriDto where IsEmriID= @IsEmriID and Silindi=0 and " +
                            "IsSorumluID=(select KaynakID from Kullanici where KullaniciID=@KullaniciID)", new { IsEmriID, KullaniciID });
            return list.FirstOrDefault();
        }

        public List<IsEmriDto> GetListPaginationDtoByKullaniciID(PagingParams pagingParams, int KullaniciID)
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

            return new DpDtoRepositoryBase<IsEmriDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_IsEmriDto where Silindi=0 
                                    and (IsSorumluID = (select KaynakID from Kullanici where KullaniciID=@KullaniciID))
                                    and IsSorumluID!=0
                                    {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { KullaniciID, pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDtoByKullaniciID(int KullaniciID, string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_IsEmriDto where Silindi=0 
                                    and (IsSorumluID = (select KaynakID from Kullanici where KullaniciID=@KullaniciID))    
                {filterQuery} ", new { KullaniciID }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public int GetEditYetki(int IsEmriID, int KullaniciID)
        {
            var strCount = GetScalarQuery($@"select COUNT(*) from IsEmri where IsEmriID = @IsEmriID and Silindi = 0 and IsTipiID IN(select IsTipiID from IsTalebiOnayBirim
                                                where KullaniciID = @KullaniciID and Silindi = 0)
                                                or (IsSorumluID = (select KaynakID from Kullanici where KullaniciID=@KullaniciID) and StatuID = 15) ", new { IsEmriID, KullaniciID }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public int Update(IsEmri isemri)
        {
            return UpdateQuery("update IsEmri set IsEmriTuruID=@IsEmriTuruID,VarlikID=@VarlikID,IsTipiID=@IsTipiID," +
                               "BakimArizaKoduID=@BakimArizaKoduID,BakimOncelikID=@BakimOncelikID,KisimID=@KisimID," +
                               "SarfyeriID=@SarfyeriID,TalepEdenID=@TalepEdenID," +
                               "PlanlananBaslangicTarih=@PlanlananBaslangicTarih," +
                               "PlanlananBaslangicSaat=@PlanlananBaslangicSaat,PlanlananBitisTarih=@PlanlananBitisTarih," +
                               "PlanlananBitisSaat=@PlanlananBitisSaat,ArizaOlusmaTarih=@ArizaOlusmaTarih," +
                               "ArizaOlusmaSaat=@ArizaOlusmaSaat,BildirilisTarih=@BildirilisTarih," +
                               "BildirilisSaat=@BildirilisSaat,BaslangicTarih=@BaslangicTarih,BaslangicSaat=@BaslangicSaat," +
                               "BitisTarih=@BitisTarih,BitisSaat=@BitisSaat,DevreyeAlmaTarih=@DevreyeAlmaTarih," +
                               "DevreyeAlmaSaat=@DevreyeAlmaSaat,IsSorumluID=@IsSorumluID,ArizaNedeniID=@ArizaNedeniID," +
                               "ArizaCozumuID=@ArizaCozumuID,YapilanIsAciklama=@YapilanIsAciklama," +
                               "TalepAciklamasi=@TalepAciklamasi,StatuID=@StatuID,StatuAciklama=@StatuAciklama," +
                               "BakimEkibiID=@BakimEkibiID,VardiyaID=@VardiyaID,IsEmircisi=@IsEmircisi," +
                               "BakimDurumuID=@BakimDurumuID,BakimAciklamasi=@BakimAciklamasi,Silindi=@Silindi " +
                               "where IsEmriID=@IsEmriID",
                isemri);
        }
        #endregion

        public List<IsEmri> GetList()
        {
            return GetListQuery("select * from IsEmri where Silindi=0", new { });
        }

        public IsEmri Get(int Id)
        {
            return GetQuery("select * from View_IsEmriDto where IsEmriID= @Id and Silindi=0", new { Id });
        }

        public int Add(IsEmri isemri)
        {
            return AddQuery("insert into IsEmri(IsEmriTuruID,VarlikID,IsTipiID,BakimArizaKoduID,BakimOncelikID,KisimID," +
                            "SarfyeriID,TalepEdenID,PlanlananBaslangicTarih,PlanlananBaslangicSaat,PlanlananBitisTarih," +
                            "PlanlananBitisSaat,ArizaOlusmaTarih,ArizaOlusmaSaat,BildirilisTarih,BildirilisSaat," +
                            "BaslangicTarih,BaslangicSaat,BitisTarih,BitisSaat,DevreyeAlmaTarih,DevreyeAlmaSaat," +
                            "IsSorumluID,ArizaNedeniID,ArizaCozumuID,YapilanIsAciklama,TalepAciklamasi,StatuID," +
                            "StatuAciklama,BakimEkibiID,VardiyaID,IsEmircisi,BakimDurumuID,BakimAciklamasi,Silindi) values " +
                            "(@IsEmriTuruID,@VarlikID,@IsTipiID,@BakimArizaKoduID,@BakimOncelikID,@KisimID,@SarfyeriID," +
                            "@TalepEdenID,@PlanlananBaslangicTarih,@PlanlananBaslangicSaat,@PlanlananBitisTarih," +
                            "@PlanlananBitisSaat,@ArizaOlusmaTarih,@ArizaOlusmaSaat,@BildirilisTarih,@BildirilisSaat," +
                            "@BaslangicTarih,@BaslangicSaat,@BitisTarih,@BitisSaat,@DevreyeAlmaTarih,@DevreyeAlmaSaat," +
                            "@IsSorumluID,@ArizaNedeniID,@ArizaCozumuID,@YapilanIsAciklama,@TalepAciklamasi,@StatuID," +
                            "@StatuAciklama,@BakimEkibiID,@VardiyaID,@IsEmircisi,@BakimDurumuID,@BakimAciklamasi,@Silindi)", isemri);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from IsEmri where IsEmriID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update IsEmri set Silindi = 1 where IsEmriID=@Id", new { Id });
        }

        public List<IsEmri> GetListPagination(PagingParams pagingParams)
        {
            string filterQuery = Datatables.FilterFabric(pagingParams.filter);
            string orderQuery = "ORDER BY 1";

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            return GetListQuery($@"SELECT * FROM IsEmri where Silindi=0 {filterQuery} {orderQuery}
OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM IsEmri where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<IsEmriDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<IsEmriDto>().GetListDtoQuery($@"SELECT {columnsQuery} FROM View_IsEmriDto where Silindi=0 
                                    and IsTipiID IN (select IsTipiID from IsTalebiOnayBirim where Silindi=0)
                                    {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filter, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filter = "")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_IsEmriDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciID(int KullaniciID)
        {
            return new DpDtoRepositoryBase<IsTipiForKullaniciTemp>().GetListDtoQuery($@"select a.IsTipiID, b.Ad as IsTipiAd, b.Kod from IsTalebiOnayBirim a inner join IsTipi b on a.IsTipiID=b.IsTipiID where a.KullaniciID=@KullaniciID and a.Silindi=0",
                new { KullaniciID });
        }

        public List<IsEmriNo> GetIsEmriNoByIsEmriID(int IsEmriID)
        {
            var list = new DpDtoRepositoryBase<IsEmriNo>().GetListDtoQuery("select * from IsEmriNo where IsEmriID= @IsEmriID and Silindi=0", new { IsEmriID });
            return list;
        }
    }
}