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
    public class RaporVarlikByVarlikGrupManager : IRaporVarlikByVarlikGrupService
    {
        IRaporVarlikByVarlikGrupDal _raporVarlikByVarlikGrupDal;

        public RaporVarlikByVarlikGrupManager(IRaporVarlikByVarlikGrupDal raporVarlikByVarlikGrupDal)
        {
            _raporVarlikByVarlikGrupDal = raporVarlikByVarlikGrupDal;
        }

        //VarlikGrupID ye göre gelen Varlıklar için pagination
        [SecuredOperation(Roles = "Admin, RaporRead, RaporVarlikByVarlikGrupRead, VarliklarLtd")]
        public int GetCountDtoByVarlikGrupID(int VarlikGrupID, string filter = "")
        {
            return _raporVarlikByVarlikGrupDal.GetCountDtoByVarlikGrupID(VarlikGrupID, filter);
        }

        //VarlikGrupID ye göre gelen Varlıkların count
        [SecuredOperation(Roles = "Admin, RaporRead, RaporVarlikByVarlikGrupRead, VarliklarLtd")]
        public List<VarlikDto> GetListPaginationDtoByVarlikGrupID(int VarlikGrupID, PagingParams pagingParams)
        {
            return _raporVarlikByVarlikGrupDal.GetListPaginationDtoByVarlikGrupID(VarlikGrupID, pagingParams);
        }
    }
}
