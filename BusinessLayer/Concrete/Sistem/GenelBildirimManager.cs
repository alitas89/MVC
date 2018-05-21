using System.Collections.Generic;
using BusinessLayer.Abstract.Sistem;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Concrete.Sistem
{
    public class GenelBildirimManager : IGenelBildirimService
    {
        IGenelBildirimDal _genelBildirimDal;

        public GenelBildirimManager(IGenelBildirimDal genelBildirimDal)
        {
            _genelBildirimDal = genelBildirimDal;
        }
        
        [SecuredOperation(Roles = "Admin, SistemRead, IsEmriRead")]
        public int GetAcikOnaysizIsTalepSayisi(int KullaniciID)
        {
            return _genelBildirimDal.GetAcikOnaysizIsTalepSayisi(KullaniciID);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, IsEmriRead")]
        public int GetAcikIsEmriSayisi(int KullaniciID)
        {
            return _genelBildirimDal.GetAcikIsEmriSayisi(KullaniciID);
        }

        [SecuredOperation(Roles = "Admin, Authorized")]
        public int GetSorumluOlunanIsEmriSayisi(int KullaniciID)
        {
            return _genelBildirimDal.GetSorumluOlunanIsEmriSayisi(KullaniciID);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, IsEmriRead")]
        public List<IsEmriBakimSonucBildirimTemp> GetIsEmriBakimSonucBildirim(int KullaniciID)
        {
            return _genelBildirimDal.GetIsEmriBakimSonucBildirim(KullaniciID);
        }
    }
}