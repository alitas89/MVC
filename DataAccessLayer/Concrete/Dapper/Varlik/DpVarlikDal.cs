using Core.DataAccessLayer.Dapper;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpVarlikDal : DpEntityRepositoryBase<EntityLayer.Concrete.Varlik.Varlik>, IVarlikDal
    {
        public List<EntityLayer.Concrete.Varlik.Varlik> GetList()
        {
            return GetListQuery("select * from Varlik where Silindi=0", new { });
        }

        public EntityLayer.Concrete.Varlik.Varlik Get(int Id)
        {
            return GetQuery("select * from Varlik where VarlikID= @Id and Silindi=0", new { Id });
        }

        public int Add(EntityLayer.Concrete.Varlik.Varlik varlik)
        {
            return AddQuery("insert into Varlik(Kod,Ad,VarlikDurumID,VarlikTuruID,VarlikGrupID,BagliVarlikKod,KisimID,SarfYeriID,IsletmeID,MarkaID,ModelID,SeriNo,BarkodKod,GarantiBitisTarih,SonKullanimTarih,Aciklama,ZimmetliPersonelID,VarsayilanIsTipiID,VarsayilanBakimArizaID,VarsayilanArizaNedenID,VarsayilanArizaCozumID,EkipID,IsEmriTurID,Silindi) values (@Kod,@Ad,@VarlikDurumID,@VarlikTuruID,@VarlikGrupID,@BagliVarlikKod,@KisimID,@SarfYeriID,@IsletmeID,@MarkaID,@ModelID,@SeriNo,@BarkodKod,@GarantiBitisTarih,@SonKullanimTarih,@Aciklama,@ZimmetliPersonelID,@VarsayilanIsTipiID,@VarsayilanBakimArizaID,@VarsayilanArizaNedenID,@VarsayilanArizaCozumID,@EkipID,@IsEmriTurID,@Silindi)", varlik);
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

            return GetListQuery($@"SELECT * FROM Varlik where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Varlik where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<VarlikDto> GetListDto()
        {
            return new DpDtoRepositoryBase<VarlikDto>().GetListDtoQuery("SELECT * FROM View_VarlikDto where Silindi=0", new { });
        }

        public List<VarlikDto> GetListPaginationDto(PagingParams pagingParams)
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
            
            return new DpDtoRepositoryBase<VarlikDto>().GetListDtoQuery($@"SELECT * FROM View_VarlikDto where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_VarlikDto where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

    }
}