using System.Collections.Generic;
using System.Linq;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.DataAccessLayer.Dapper.RepositoryInterface;
using DataAccessLayer.Abstract.DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpSarfYeriDal : DpEntityRepositoryBase<SarfYeri>, ISarfYeriDal
    {
        public List<SarfYeri> GetList()
        {
            return GetListQuery($"select * from SarfYeri where Silindi=0", new { });
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
            return new DpDtoRepositoryBase<SarfYeriDto>().GetListDtoQuery("select SY.*, I.Ad as IsletmeAd from SarfYeri as SY inner join Isletme as I ON SY.IsletmeID = I.IsletmeID", new { });
        }
    }
}