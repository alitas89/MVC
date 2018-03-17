using System.Collections.Generic;
using BusinessLayer.Abstract.Genel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Concrete.Genel
{
    public class VerifyManager : IVerifyService
    {
        IVerifyDal _verifyDal;

        public VerifyManager(IVerifyDal verifyDal)
        {
            _verifyDal = verifyDal;
        }

        /// <summary>
        /// Bu metod ile token doğrulaması yapılır.
        /// SecuredOperation vasıtasıyla tüm roller yazılır.
        /// Herhangi bir role sahip olan kullanıcı yetkilendirilmiştir.
        /// </summary>
        /// <returns></returns>
        [SecuredOperation(Roles = "Authorized")]
        public List<Verify> GetList()
        {
            return _verifyDal.GetList();
        }
    }
}
