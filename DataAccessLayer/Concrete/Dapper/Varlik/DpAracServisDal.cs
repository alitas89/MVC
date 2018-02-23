﻿using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace DataAccessLayer.Concrete.Dapper.Varlik
{
    public class DpAracServisDal : DpEntityRepositoryBase<AracServis>, IAracServisDal
    {
        public List<AracServis> GetList()
        {
            return GetListQuery("select * from AracServis where Silindi=0", new { });
        }

        public AracServis Get(int Id)
        {
            return GetQuery("select * from AracServis where AracServisID= @Id and Silindi=0", new { Id });
        }

        public int Add(AracServis aracServis)
        {
            return AddQuery("insert into AracServis(IsEmriYili,FisNo,TalepEdenID,AracID,GorevID,TalepTarih,TalepSaat,TeslimEtmeTarih,TeslimEtmeSaat,TeslimAlmaTarih,TeslimAlmaSaat,TeslimAlinanKm,TeslimEdilenKm,KullanilanKm,Aciklama,TeslimEdenID,TeslimAlanID,TeslimAmbarID,BolumID,VarlikDurumID,MarkaID,ModelID,SeriNo,ArizaID,HizmetID,ServisAdres,Silindi) values (@IsEmriYili,@FisNo,@TalepEdenID,@AracID,@GorevID,@TalepTarih,@TalepSaat,@TeslimEtmeTarih,@TeslimEtmeSaat,@TeslimAlmaTarih,@TeslimAlmaSaat,@TeslimAlinanKm,@TeslimEdilenKm,@KullanilanKm,@Aciklama,@TeslimEdenID,@TeslimAlanID,@TeslimAmbarID,@BolumID,@VarlikDurumID,@MarkaID,@ModelID,@SeriNo,@ArizaID,@HizmetID,@ServisAdres,@Silindi)", aracServis);
        }

        public int Update(AracServis aracServis)
        {
            return UpdateQuery("update AracServis set IsEmriYili=@IsEmriYili,FisNo=@FisNo,TalepEdenID=@TalepEdenID,AracID=@AracID,GorevID=@GorevID,TalepTarih=@TalepTarih,TalepSaat=@TalepSaat,TeslimEtmeTarih=@TeslimEtmeTarih,TeslimEtmeSaat=@TeslimEtmeSaat,TeslimAlmaTarih=@TeslimAlmaTarih,TeslimAlmaSaat=@TeslimAlmaSaat,TeslimAlinanKm=@TeslimAlinanKm,TeslimEdilenKm=@TeslimEdilenKm,KullanilanKm=@KullanilanKm,Aciklama=@Aciklama,TeslimEdenID=@TeslimEdenID,TeslimAlanID=@TeslimAlanID,TeslimAmbarID=@TeslimAmbarID,BolumID=@BolumID,VarlikDurumID=@VarlikDurumID,MarkaID=@MarkaID,ModelID=@ModelID,SeriNo=@SeriNo,ArizaID=@ArizaID,HizmetID=@HizmetID,ServisAdres=@ServisAdres,Silindi=@Silindi where AracServisID=@AracServisID", aracServis);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from AracServis where AracServisID=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update AracServis set Silindi = 1 where AracServisID=@Id", new { Id });
        }

        public List<AracServis> GetListPagination(PagingParams pagingParams)
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

            return GetListQuery($@"SELECT * FROM AracServis where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM AracServis where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<AracServisDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<AracServisDto>().GetListDtoQuery($@"SELECT * FROM View_AracServisDto where Silindi=0 {filterQuery} {orderQuery}
                OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCountDto(string filterCol = "", string filterVal = "")
        {
            string filter = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                filter = $"and {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_AracServisDto where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}
