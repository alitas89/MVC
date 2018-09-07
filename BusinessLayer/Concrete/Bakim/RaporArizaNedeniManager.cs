using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.Bakim
{
   public class RaporArizaNedeniManager : IRaporArizaNedeniService
    {
        IRaporArizaNedeniDal _raporArizaNedeniDal;

        public RaporArizaNedeniManager(IRaporArizaNedeniDal raporArizaNedeniDal)
        {
            _raporArizaNedeniDal = raporArizaNedeniDal;
        }

        //VarlikID ye göre gelen IsEmirleri için 
        [SecuredOperation(Roles = "Admin, RaporRead, RaporArizaNedeniRead,ArizaNedeniLtd")]
        public int GetCountDto(string filter = "")
        {
            return _raporArizaNedeniDal.GetCountDto(filter);
        }

        //VarlikID ye göre gelen IsEmirleri için 
        [SecuredOperation(Roles = "Admin, RaporRead, RaporArizaNedeniRead, ArizaNedeniLtd")]
        public List<ArizaNedeni> GetListPaginationDto( PagingParams pagingParams)
        {
            return _raporArizaNedeniDal.GetListPaginationDto(pagingParams);
        }
    }
}
