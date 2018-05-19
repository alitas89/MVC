using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Sistem;
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
            var strCount = GetScalarQuery($@"Select COUNT(*) from IsEmri where IsSorumluID=(select KaynakID from Kullanici where KullaniciID=@KullaniciID) and StatuID=15 ",
                               new { KullaniciID }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }
    }
}