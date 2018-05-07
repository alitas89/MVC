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
                            and IsEmriID is null and b.IsTipiID in(
                        	select IsTipiID from IsTalebiOnayBirim where KullaniciID=@KullaniciID and Silindi=0
            ) ", new { KullaniciID  }) + "";

            int.TryParse(strCount, out int count);
            return count;
        }
    }
}