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
    public class RaporArizaNedeniByVarlikManager : IRaporArizaNedeniByVarlikService
    {
        IRaporArizaNedeniByVarlikDal _raporArizaNedeniByVarlikDal;

        public RaporArizaNedeniByVarlikManager(IRaporArizaNedeniByVarlikDal raporArizaNedeniByVarlikDal)
        {
            _raporArizaNedeniByVarlikDal = raporArizaNedeniByVarlikDal;
        }

        [SecuredOperation(Roles = "Admin, RaporRead, RaporArizaNedeniByVarlikRead, ArizaNedeniLtd")]
        public int GetCountDtoByVarlikID(int VarlikID, string filter = "")
        {
            return _raporArizaNedeniByVarlikDal.GetCountDtoByVarlikID(VarlikID, filter);
        }

        [SecuredOperation(Roles = "Admin, RaporRead, RaporArizaNedeniByVarlikRead, ArizaNedeniLtd")]
        public List<ArizaNedeni> GetListPaginationDtoByVarlikID(int VarlikID, PagingParams pagingParams)
        {
            return _raporArizaNedeniByVarlikDal.GetListPaginationDtoByVarlikID(VarlikID, pagingParams);
        }
    }
}
