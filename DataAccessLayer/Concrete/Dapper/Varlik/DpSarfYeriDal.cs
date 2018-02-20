﻿using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Varlik.DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpSarfYeriDal : DpEntityRepositoryBase<SarfYeri>, ISarfYeriDal
    {
        public List<SarfYeri> GetList()
        {
            return GetListQuery($"select * from SarfYeri where Silindi=0", new { });
        }

        public List<SarfYeri> GetList(int IsletmeID)
        {
            return GetListQuery($"select * from SarfYeri where Silindi=0 and IsletmeID=@IsletmeID", new { IsletmeID });
        }

        public SarfYeri Get(int Id)
        {
            return GetQuery("select * from SarfYeri where SarfYeriID= @Id and Silindi=0", new { Id });
        }

        public int Add(SarfYeri sarfyeri)
        {
            return AddQuery("insert into SarfYeri(Kod,Ad,Butce,HedeflenenButce,VardiyaSinifID,IsletmeID,Telefon1,Telefon2,FaxNo,Email,WebUrl,LogoDosyaYolu,Aciklama,SatinAlmaYeri,Silindi) values (@Kod,@Ad,@Butce,@HedeflenenButce,@VardiyaSinifID,@IsletmeID,@Telefon1,@Telefon2,@FaxNo,@Email,@WebUrl,@LogoDosyaYolu,@Aciklama,@SatinAlmaYeri,@Silindi)", sarfyeri);
        }

        public int Update(SarfYeri sarfyeri)
        {
            return UpdateQuery("update SarfYeri set Kod=@Kod,Ad=@Ad,Butce=@Butce,HedeflenenButce=@HedeflenenButce,VardiyaSinifID=@VardiyaSinifID,IsletmeID=@IsletmeID,Telefon1=@Telefon1,Telefon2=@Telefon2,FaxNo=@FaxNo,Email=@Email,WebUrl=@WebUrl,LogoDosyaYolu=@LogoDosyaYolu,Aciklama=@Aciklama,SatinAlmaYeri=@SatinAlmaYeri,Silindi=@Silindi where SarfYeriID=@SarfYeriID", sarfyeri);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from SarfYeri where SarfYeriID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update SarfYeri set Silindi = 1 where SarfYeriID=@Id", new { Id });
        }

        public List<SarfYeriDto> GetListDto()
        {
            return new DpDtoRepositoryBase<SarfYeriDto>().GetListDtoQuery("select SY.*, I.Ad as IsletmeAd from SarfYeri as SY inner join Isletme as I ON SY.IsletmeID = I.IsletmeID where SY.Silindi=0", new { });
        }

        public List<SarfYeri> GetListPagination(PagingParams pagingParams)
        {
            string filterQuery = "";
            string orderQuery = "ORDER BY 1";
            if (pagingParams.filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                pagingParams.filterVal = '%' + pagingParams.filterVal + '%';
                filterQuery = $"and {pagingParams.filterCol} like @filterVal";
            }

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            return GetListQuery($@"SELECT * FROM SarfYeri where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string filter = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                filter = $"and {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM SarfYeri where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<SarfYeriDto> GetListPaginationDto(PagingParams pagingParams)
        {
            string filterQuery = "";
            string orderQuery = "ORDER BY 1";
            if (pagingParams.filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                pagingParams.filterVal = '%' + pagingParams.filterVal + '%';
                if (pagingParams.filterCol.Equals("IsletmeAd"))
                {
                    filterQuery = $"and I.{pagingParams.filterCol} like @filterVal";
                }
                else
                {
                    filterQuery = $"and SY.{pagingParams.filterCol} like @filterVal";
                } 
            }

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                if (arrOrder[0].ToString().Equals("IsletmeAd"))
                {
                    orderQuery = $"ORDER BY I.{arrOrder[0]} {arrOrder[1]}";
                }
                else
                {
                    orderQuery = $"ORDER BY SY.{arrOrder[0]} {arrOrder[1]}";
                }
            }

            return new DpDtoRepositoryBase<SarfYeriDto>().GetListDtoQuery($@"select SY.*, I.Ad as IsletmeAd from SarfYeri as SY inner join Isletme as I ON SY.IsletmeID = I.IsletmeID where SY.Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public bool IsKodDefined(string Kod)
        {
            var result = GetScalarQuery("select Count(*) from SarfYeri where Kod= @Kod and Silindi=0", new { Kod }) + "";
            int.TryParse(result, out int count);
            return count > 0;
        }
    }
}