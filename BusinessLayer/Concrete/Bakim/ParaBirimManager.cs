using System.Collections.Generic;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class ParaBirimManager : IParaBirimService
    {
        IParaBirimDal _parabirimDal;

        public ParaBirimManager(IParaBirimDal parabirimDal)
        {
            _parabirimDal = parabirimDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, ParaBirimRead, ParaBirimLtd")]
        public List<ParaBirim> GetList()
        {
            return _parabirimDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, ParaBirimRead, ParaBirimLtd")]
        public ParaBirim GetById(int Id)
        {
            return _parabirimDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, ParaBirimCreate")]
        public int Add(ParaBirim parabirim)
        {
            return _parabirimDal.Add(parabirim);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, ParaBirimUpdate")]
        public int Update(ParaBirim parabirim)
        {
            return _parabirimDal.Update(parabirim);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, ParaBirimDelete")]
        public int Delete(int Id)
        {
            return _parabirimDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, ParaBirimDelete")]
        public int DeleteSoft(int Id)
        {
            return _parabirimDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, ParaBirimRead, ParaBirimLtd")]
        public List<ParaBirim> GetListPagination(PagingParams pagingParams)
        {
            return _parabirimDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _parabirimDal.GetCount(filter);
        }
    }
}