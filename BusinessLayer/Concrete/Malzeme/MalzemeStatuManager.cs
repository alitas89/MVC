using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Malzeme;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Concrete.Malzeme
{
    public class MalzemeStatuManager : IMalzemeStatuService
    {
        IMalzemeStatuDal _malzemestatuDal;

        public MalzemeStatuManager(IMalzemeStatuDal malzemestatuDal)
        {
            _malzemestatuDal = malzemestatuDal;
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeStatuRead, MalzemeStatuLtd")]
        public List<MalzemeStatu> GetList()
        {
            return _malzemestatuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeStatuRead, MalzemeStatuLtd")]
        public MalzemeStatu GetById(int Id)
        {
            return _malzemestatuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeCreate, MalzemeStatuCreate")]
        public int Add(MalzemeStatu malzemestatu)
        {
            return _malzemestatuDal.Add(malzemestatu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeUpdate, MalzemeStatuUpdate")]
        public int Update(MalzemeStatu malzemestatu)
        {
            return _malzemestatuDal.Update(malzemestatu);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemeStatuDelete")]
        public int Delete(int Id)
        {
            return _malzemestatuDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemeStatuDelete")]
        public int DeleteSoft(int Id)
        {
            return _malzemestatuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeStatuRead, MalzemeStatuLtd")]
        public List<MalzemeStatu> GetListPagination(PagingParams pagingParams)
        {
            return _malzemestatuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _malzemestatuDal.GetCount(filter);
        }

    }
}
