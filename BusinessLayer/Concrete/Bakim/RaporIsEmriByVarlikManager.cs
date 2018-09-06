using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.Bakim
{
    public class RaporIsEmriByVarlikManager:IRaporIsEmriByVarlikService
    {
        IRaporIsEmriByVarlikDal _raporIsEmriByVarlikDal;

        public RaporIsEmriByVarlikManager(IRaporIsEmriByVarlikDal raporIsEmriByVarlikDal)
        {
            _raporIsEmriByVarlikDal = raporIsEmriByVarlikDal;
        }

        //VarlikID ye göre gelen IsEmirleri için 
        [SecuredOperation(Roles = "Admin, RaporRead, RaporIsEmriByVarlikRead, IsEmriLtd")]
        public int GetCountDtoByVarlikID(int VarlikID, string filter = "")
        {
            return _raporIsEmriByVarlikDal.GetCountDtoByVarlikID(VarlikID, filter);
        }

        //VarlikID ye göre gelen IsEmirleri için 
        [SecuredOperation(Roles = "Admin, RaporRead, RaporIsEmriByVarlikRead, IsEmriLtd")]
        public List<IsEmriDto> GetListPaginationDtoByVarlikID(int VarlikID, PagingParams pagingParams)
        {
            return _raporIsEmriByVarlikDal.GetListPaginationDtoByVarlikID(VarlikID, pagingParams);
        }

       
    }
}
