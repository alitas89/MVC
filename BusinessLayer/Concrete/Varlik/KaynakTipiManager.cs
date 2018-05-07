using System.Collections.Generic;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Concrete.Varlik
{
    public class KaynakTipiManager : IKaynakTipiService
    {
        IKaynakTipiDal _kaynaktipiDal;

        public KaynakTipiManager(IKaynakTipiDal kaynaktipiDal)
        {
            _kaynaktipiDal = kaynaktipiDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, KaynakTipiRead, KaynakTipiLtd")]
        public List<KaynakTipi> GetList()
        {
            return _kaynaktipiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, KaynakTipiRead, KaynakTipiLtd")]
        public KaynakTipi GetById(int Id)
        {
            return _kaynaktipiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikCreate, KaynakTipiCreate")]
        public int Add(KaynakTipi kaynaktipi)
        {
            return _kaynaktipiDal.Add(kaynaktipi);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, KaynakTipiUpdate")]
        public int Update(KaynakTipi kaynaktipi)
        {
            return _kaynaktipiDal.Update(kaynaktipi);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, KaynakTipiDelete")]
        public int Delete(int Id)
        {
            return _kaynaktipiDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, KaynakTipiDelete")]
        public int DeleteSoft(int Id)
        {
            return _kaynaktipiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, KaynakTipiRead, KaynakTipiLtd")]
        public List<KaynakTipi> GetListPagination(PagingParams pagingParams)
        {
            return _kaynaktipiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _kaynaktipiDal.GetCount(filter);
        }

    }
}