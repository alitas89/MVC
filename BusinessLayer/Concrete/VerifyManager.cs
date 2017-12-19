using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.AuthorizationAspects;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
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
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<Verify> GetList()
        {
            return _verifyDal.GetList();
        }
    }
}
