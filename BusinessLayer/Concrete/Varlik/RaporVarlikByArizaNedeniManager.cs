using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.Varlik
{
    class RaporVarlikByArizaNedeniManager : IRaporVarlikByArizaNedeniService
    {
        IRaporVarlikByArizaNedeniDal _raporVarlikByArizaNedeniDal;

        public RaporVarlikByArizaNedeniManager(IRaporVarlikByArizaNedeniDal raporVarlikByArizaNedeniDal)
        {
            _raporVarlikByArizaNedeniDal = raporVarlikByArizaNedeniDal;
        }

        //ArizaNedeniID ye göre gelen Varlıklar için pagination
        [SecuredOperation(Roles = "Admin, RaporRead, RaporVarlikByArizaNedeniRead, VarliklarLtd")]
        public int GetCountDtoByArizaNedeniID(int ArizaNedeniID, string filter = "")
        {
            return _raporVarlikByArizaNedeniDal.GetCountDtoByArizaNedeniID(ArizaNedeniID, filter);
        }

        //ArizaNedeniID ye göre gelen Varlıkların count
        [SecuredOperation(Roles = "Admin, RaporRead, RaporVarlikByArizaNedeniRead, VarliklarLtd")]
        public List<VarlikDto> GetListPaginationDtoByArizaNedeniID(int ArizaNedeniID, PagingParams pagingParams)
        {
            return _raporVarlikByArizaNedeniDal.GetListPaginationDtoByArizaNedeniID(ArizaNedeniID, pagingParams);
        }
    }
}
