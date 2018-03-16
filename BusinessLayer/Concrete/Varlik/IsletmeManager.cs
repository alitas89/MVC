using System.Collections.Generic;
using BusinessLayer.Abstract.Varlik;
using BusinessLayer.ValidationRules.FluentValidation;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.ValidationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class IsletmeManager : IIsletmeService
    {
        IIsletmeDal _isletmeDal;

        public IsletmeManager(IIsletmeDal isletmeDal)
        {
            _isletmeDal = isletmeDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikRead, IsletmeRead, IsletmeLtd")]
        public List<Isletme> GetList()
        {
            return _isletmeDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, IsletmeRead, IsletmeLtd")]
        public Isletme GetById(int Id)
        {
            return _isletmeDal.Get(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(IsletmeValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikCreate, IsletmeCreate")]
        public int Add(Isletme isletme)
        {
            //Kod Kontrolü - Aynı koda sahip kayıt varsa ekleme yapılamaz!
            return _isletmeDal.IsKodDefined(isletme.Kod) ? 0 : _isletmeDal.Add(isletme);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(IsletmeValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, IsletmeUpdate")]
        public int Update(Isletme isletme)
        {
            //Kod Kontrolü - Aynı koda sahip kayıt varsa güncelleme yapılamaz! (Kendisi dışındaki bir kod olmalı)
            if (_isletmeDal.IsKodDefined(isletme.Kod))
            {
                //Var olan kod kendi kodu mu?
                return _isletmeDal.Get(isletme.IsletmeID).Kod == isletme.Kod ? _isletmeDal.Update(isletme) : 0;
            }
            else
            {
                return _isletmeDal.Update(isletme);
            }
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, IsletmeDelete")]
        public int Delete(int Id)
        {
            return _isletmeDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, IsletmeDelete")]
        public int DeleteSoft(int Id)
        {
            return _isletmeDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, IsletmeRead, IsletmeLtd")]
        public List<Isletme> GetListPagination(PagingParams pagingParams)
        {
            return _isletmeDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _isletmeDal.GetCount(filter);
        }
    }
}
