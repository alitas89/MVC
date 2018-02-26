using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

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

            return GetListQuery($@"SELECT * FROM AkaryakitAlimFis where Silindi=0 {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
            new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string where = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                where = $" where {filterCol} like @filterVal";
            }
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM AkaryakitAlimFis {where}", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<AkaryakitAlimFisDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<AkaryakitAlimFisDto>().GetListDtoQuery($@"SELECT * FROM View_AkaryakitAlimFisDto where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_AkaryakitAlimFisDto where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}

       