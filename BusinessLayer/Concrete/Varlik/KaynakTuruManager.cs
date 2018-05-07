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
    public class KaynakTuruManager : IKaynakTuruService
    {
        IKaynakTuruDal _kaynakturuDal;

        public KaynakTuruManager(IKaynakTuruDal kaynakturuDal)
        {
            _kaynakturuDal = kaynakturuDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, KaynakTuruRead, KaynakTuruLtd")]
        public List<KaynakTuru> GetList()
        {
            return _kaynakturuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, KaynakTuruRead, KaynakTuruLtd")]
        public KaynakTuru GetById(int Id)
        {
            return _kaynakturuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikCreate, KaynakTuruCreate")]
        public int Add(KaynakTuru kaynakturu)
        {
            return _kaynakturuDal.Add(kaynakturu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, KaynakTuruUpdate")]
        public int Update(KaynakTuru kaynakturu)
        {
            return _kaynakturuDal.Update(kaynakturu);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, KaynakTuruDelete")]
        public int Delete(int Id)
        {
            return _kaynakturuDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, KaynakTuruDelete")]
        public int DeleteSoft(int Id)
        {
            return _kaynakturuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, KaynakTuruRead, KaynakTuruLtd")]
        public List<KaynakTuru> GetListPagination(PagingParams pagingParams)
        {
            return _kaynakturuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _kaynakturuDal.GetCount(filter);
        }

    }
}