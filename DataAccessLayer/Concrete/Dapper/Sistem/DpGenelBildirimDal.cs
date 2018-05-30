using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.Concrete.Bakim;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Concrete.Dapper.Sistem
{
    public class DpGenelBildirimDal : DpEntityRepositoryBase<GenelBildirim>, IGenelBildirimDal
    {
        public int GetAcikOnaysizIsTalepSayisi(int KullaniciID)
        {
            var strCount = GetScalarQuery($@"select COUNT(*) from IsEmriNo a inner join 
                            IsTalebi b on a.IsTalepID=b.IsTalebiID where a.Silindi=0 and b.Silindi=0 
                            and b.StatuID=7 and b.IsTipiID in(
                        	select IsTipiID from IsTalebiOnayBirim where KullaniciID=@KullaniciID and Silindi=0
            ) ", new { KullaniciID }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public int GetAcikIsEmriSayisi(int KullaniciID)
        {
            var strCount = GetScalarQuery($@"select COUNT(*) from IsEmriNo a inner join 
						IsEmri b on a.IsEmriID=b.IsEmriID where a.Silindi=0 and b.Silindi=0
						and b.StatuID=14 and b.IsTipiID in(
                        	select IsTipiID from IsTalebiOnayBirim where KullaniciID=@KullaniciID and Silindi=0) ",
                            new { KullaniciID }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public int GetSorumluOlunanIsEmriSayisi(int KullaniciID)
        {
            var strCount = GetScalarQuery($@"Select COUNT(*) from IsEmri where IsSorumluID=(select KaynakID from Kullanici where KullaniciID=@KullaniciID) and StatuID=15 and BakimDurumuID=1",
                               new { KullaniciID }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }

        public List<IsEmriBakimSonucBildirimTemp> GetIsEmriBakimSonucBildirim(int KullaniciID)
        {
            return new DpDtoRepositoryBase<IsEmriBakimSonucBildirimTemp>().GetListDtoQuery($@"
                    select b.IsEmriNoID, a.IsEmriID, a.BakimDurumuID, a.BitisTarih, a.BitisSaat, c.Ad as BakimDurumuAd  from IsEmri a inner join IsEmriNo b on a.IsEmriID=b.IsEmriID
                        inner join BakimDurumu c on a.BakimDurumuID = c.BakimDurumuID
                        where IsEmircisi=(select KaynakID from Kullanici where KullaniciID=@KullaniciID)
                        and StatuID=15 and a.BakimDurumuID in (2,3)",
                new { KullaniciID });
        }

        public List<IsTalepSonucBildirimTemp> GetIsTalepSonucBildirim(int KullaniciID)
        {
            return new DpDtoRepositoryBase<IsTalepSonucBildirimTemp>().GetListDtoQuery($@"
                    select b.IsEmriNoID, a.IsEmriID, c.IsTalebiID, c.TalepEdenID, k.KullaniciID,
                                                a.StatuID, s.Ad as StatuAd from IsEmri a 
                                inner join IsEmriNo b 
                                on a.IsEmriID = b.IsEmriID
                                inner join IsTalebi c 
                                on b.IsTalepID = c.IsTalebiID 
                                inner join Kullanici k
                                on c.TalepEdenID=k.KaynakID
                                inner join Statu s
                                on a.StatuID=s.StatuID
                                left join BildirimIsTalebiSonuc i
								on i.IsEmriNoID=b.IsEmriNoID
                                where a.StatuID in (16,17) and a.Silindi=0 and
                                i.IsEmriNoID is null and KullaniciID=@KullaniciID",
                new { KullaniciID });
        }
    }
}