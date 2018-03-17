﻿using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.Utilities.Dal;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpIsEmriDal : DpEntityRepositoryBase<IsEmri>, IIsEmriDal
    {
        public List<IsEmri> GetList()
        {
            return GetListQuery("select * from IsEmri where Silindi=0", new { });
        }

        public IsEmri Get(int Id)
        {
            return GetQuery("select * from IsEmri where IsEmriID= @Id and Silindi=0", new { Id });
        }

        public int Add(IsEmri isemri)
        {
            return AddQuery("insert into IsEmri(IsEmriNo,IsEmriTuruID,VarlikID,IsTipiID,BakimArizaKoduID,BakimOncelikID,KisimID,SarfyeriID,TalepEdenID,PlanlananBaslangicTarih,PlanlananBaslangicSaat,PlanlananBitisTarih,PlanlananBitisSaat,ArizaOlusmaTarih,ArizaOlusmaSaat,BildirilisTarih,BaslangicSaat,BitisTarih,BitisSaat,DevreyeAlmaTarih,DevreyeAlmaSaat,IsSorumluID,ArizaNedeniID,ArizaCozumuID,YapilanIsAciklama,TalepAciklamasi,StatuID,StatuAciklama,BakimEkibiID,VardiyaID,Silindi) values (@IsEmriNo,@IsEmriTuruID,@VarlikID,@IsTipiID,@BakimArizaKoduID,@BakimOncelikID,@KisimID,@SarfyeriID,@TalepEdenID,@PlanlananBaslangicTarih,@PlanlananBaslangicSaat,@PlanlananBitisTarih,@PlanlananBitisSaat,@ArizaOlusmaTarih,@ArizaOlusmaSaat,@BildirilisTarih,@BaslangicSaat,@BitisTarih,@BitisSaat,@DevreyeAlmaTarih,@DevreyeAlmaSaat,@IsSorumluID,@ArizaNedeniID,@ArizaCozumuID,@YapilanIsAciklama,@TalepAciklamasi,@StatuID,@StatuAciklama,@BakimEkibiID,@VardiyaID,@Silindi)", isemri);
        }

        public int Update(IsEmri isemri)
        {
            return UpdateQuery("update IsEmri set IsEmriNo=@IsEmriNo,IsEmriTuruID=@IsEmriTuruID,VarlikID=@VarlikID,IsTipiID=@IsTipiID,BakimArizaKoduID=@BakimArizaKoduID,BakimOncelikID=@BakimOncelikID,KisimID=@KisimID,SarfyeriID=@SarfyeriID,TalepEdenID=@TalepEdenID,PlanlananBaslangicTarih=@PlanlananBaslangicTarih,PlanlananBaslangicSaat=@PlanlananBaslangicSaat,PlanlananBitisTarih=@PlanlananBitisTarih,PlanlananBitisSaat=@PlanlananBitisSaat,ArizaOlusmaTarih=@ArizaOlusmaTarih,ArizaOlusmaSaat=@ArizaOlusmaSaat,BildirilisTarih=@BildirilisTarih,BaslangicSaat=@BaslangicSaat,BitisTarih=@BitisTarih,BitisSaat=@BitisSaat,DevreyeAlmaTarih=@DevreyeAlmaTarih,DevreyeAlmaSaat=@DevreyeAlmaSaat,IsSorumluID=@IsSorumluID,ArizaNedeniID=@ArizaNedeniID,ArizaCozumuID=@ArizaCozumuID,YapilanIsAciklama=@YapilanIsAciklama,TalepAciklamasi=@TalepAciklamasi,StatuID=@StatuID,StatuAciklama=@StatuAciklama,BakimEkibiID=@BakimEkibiID,VardiyaID=@VardiyaID,Silindi=@Silindi where IsEmriID=@IsEmriID", isemri);
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

            return new DpDtoRepositoryBase<IsEmriDto>().GetListDtoQuery($@"SELECT * FROM View_IsEmriDto where Silindi=0 {filterQuery} {orderQuery}
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
    }
}