using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.Varlik
{
    public class YakitManager : IYakitService
    {
        IYakitDal _yakitDal;

        public YakitManager(IYakitDal yakitDal)
        {
            _yakitDal = yakitDal;
        }

        
        [SecuredOperation(Roles = "Admin, YakitRead, YakitLtd")]
        public List<Yakit> GetList()
        {
            return _yakitDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, YakitRead, YakitLtd")]
        public Yakit GetById(int Id)
        {
            return _yakitDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, YakitCreate")]
        public int Add(Yakit yakit)
        {
            return _yakitDal.Add(yakit);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, YakitUpdate")]
        public int Update(Yakit yakit)
        {
            return _yakitDal.Update(yakit);
        }

        
        [SecuredOperation(Roles = "Admin, YakitDelete")]
        public int Delete(int Id)
        {
            return _yakitDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, YakitDelete")]
        public int DeleteSoft(int Id)
        {
            return _yakitDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, YakitRead, YakitLtd")]
        public List<Yakit> GetListPagination(PagingParams pagingParams)
        {
            return _yakitDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _yakitDal.GetCount(filter);
        }

    }
}
