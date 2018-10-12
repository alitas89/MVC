using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.Bakim
{
    public class RaporIsEmriSayisiveOraniManager : IRaporIsEmriSayisiveOraniService
    {
        IRaporIsEmriSayisiveOraniDal _raporisemrisayisiveoraniDal;

        public RaporIsEmriSayisiveOraniManager(IRaporIsEmriSayisiveOraniDal raporisemrisayisiveoraniDal)
        {
            _raporisemrisayisiveoraniDal = raporisemrisayisiveoraniDal;
        }
        [SecuredOperation(Roles = "Admin, RaporIsEmriSayisiveOraniRead, RaporIsEmriSayisiveOraniLtd")]
        public List<RaporIsEmriSayisiveOraniTemp> GetList()
        {
            return _raporisemrisayisiveoraniDal.GetList();
        }

    }
}
