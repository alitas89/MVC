using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;
using Core.Utilities.Dal;
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
            return AddQuery("insert into IsTalebi(TalepNo,TalepYil,IsEmriTuruID,BakimOncelikID,VarlikID,KisimID,ArizaOlusmaTarih,ArizaOlusmaSaat,BildirilisTarih,BildirilisSaat,TalepEdenID,IsTipiID,BakimArizaID,Aciklama,OnaylayanID,OnaylayanAciklama,SorumluID,EkipID,OnayTarih,OnaySaat,StatuID,Silindi) values (@TalepNo,@TalepYil,@IsEmriTuruID,@BakimOncelikID,@VarlikID,@KisimID,@ArizaOlusmaTarih,@ArizaOlusmaSaat,@BildirilisTarih,@BildirilisSaat,@TalepEdenID,@IsTipiID,@BakimArizaID,@Aciklama,@OnaylayanID,@OnaylayanAciklama,@SorumluID,@EkipID,@OnayTarih,@OnaySaat,@StatuID,@Silindi);" +
                " SELECT CAST(SCOPE_IDENTITY() as int)", ıstalebi, true);
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

        public int GetCountDto(string filter="")
        {
            string filterQuery = Datatables.FilterFabric(filter);
            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM View_IsTalebiDto where Silindi=0 {filterQuery} ", new { }) + "";
            int.TryParse(strCount, out int count);
            return count;
        }

        public List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciID(int KullaniciID)
        {
            return new DpDtoRepositoryBase<IsTipiForKullaniciTemp>().GetListDtoQuery($@"select a.IsTipiID, b.Ad as IsTipiAd from IsTalebiBirim a inner join IsTipi b on a.IsTipiID=b.IsTipiID where a.KullaniciID=@KullaniciID and a.Silindi=0",
                new { KullaniciID });
        }
    }
}
