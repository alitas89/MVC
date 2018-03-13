using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;
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
            return AddQuery("insert into IsTalebi(TalepNo,TalepYil,IsEmriTuruID,BakimOncelikID,VarlikID,KisimID,ArizaOlusmaTarih,ArizaOlusmaSaat,BildirilisTarih,BildirilisSaat,TalepEdenID,IsTipiID,BakimArizaID,Aciklama,OnaylayanID,OnaylayanAciklama,SorumluID,EkipID,OnayTarih,OnaySaat,StatuID,Silindi) values (@TalepNo,@TalepYil,@IsEmriTuruID,@BakimOncelikID,@VarlikID,@KisimID,@ArizaOlusmaTarih,@ArizaOlusmaSaat,@BildirilisTarih,@BildirilisSaat,@TalepEdenID,@IsTipiID,@BakimArizaID,@Aciklama,@OnaylayanID,@OnaylayanAciklama,@SorumluID,@EkipID,@OnayTarih,@OnaySaat,@StatuID,@Silindi)", ıstalebi);
        }

        public int Update(IsTalebi ıstalebi)
        {
            return UpdateQuery("update IsTalebi set TalepNo=@TalepNo,TalepYil=@TalepYil,IsEmriTuruID=@IsEmriTuruID,BakimOncelikID=@BakimOncelikID,VarlikID=@VarlikID,KisimID=@KisimID,ArizaOlusmaTarih=@ArizaOlusmaTarih,ArizaOlusmaSaat=@ArizaOlusmaSaat,BildirilisTarih=@BildirilisTarih,BildirilisSaat=@BildirilisSaat,TalepEdenID=@TalepEdenID,IsTipiID=@IsTipiID,BakimArizaID=@BakimArizaID,Aciklama=@Aciklama,OnaylayanID=@OnaylayanID,OnaylayanAciklama=@OnaylayanAciklama,SorumluID=@SorumluID,EkipID=@EkipID,OnayTarih=@OnayTarih,OnaySaat=@OnaySaat,StatuID=@StatuID,Silindi=@Silindi where IsTalebiID=@IsTalebiID", ıstalebi);
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

            return GetListQuery($@"SELECT * FROM IsTalebi where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM IsTalebi where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<IsTalebiDto> GetListPaginationDto(PagingParams pagingParams)
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

            return new DpDtoRepositoryBase<IsTalebiDto>().GetListDtoQuery($@"SELECT * FROM View_IsTalebiDto where Silindi=0 {filterQuery} {orderQuery}
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
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_IsTalebiDto where Silindi=0 {filter} ", new { filterVal }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }
    }
}
