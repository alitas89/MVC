using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Concrete.Varlik
{
    public class KaynakDurumuManager : IKaynakDurumuService
    {
        IKaynakDurumuDal _kaynakdurumuDal;

        public KaynakDurumuManager(IKaynakDurumuDal kaynakdurumuDal)
        {
            _kaynakdurumuDal = kaynakdurumuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikRead, KaynakDurumuRead, KaynakDurumuLtd")]
        public List<KaynakDurumu> GetList()
        {
            return _kaynakdurumuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, KaynakDurumuRead, KaynakDurumuLtd")]
        public KaynakDurumu GetById(int Id)
        {
            return _kaynakdurumuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikCreate, KaynakDurumuCreate")]
        public int Add(KaynakDurumu kaynakdurumu)
        {
            return _kaynakdurumuDal.Add(kaynakdurumu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, KaynakDurumuUpdate")]
        public int Update(KaynakDurumu kaynakdurumu)
        {
            return _kaynakdurumuDal.Update(kaynakdurumu);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, KaynakDurumuDelete")]
        public int Delete(int Id)
        {
            return _kaynakdurumuDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, KaynakDurumuDelete")]
        public int DeleteSoft(int Id)
        {
            return _kaynakdurumuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, KaynakDurumuRead, KaynakDurumuLtd")]
        public List<KaynakDurumu> GetListPagination(PagingParams pagingParams)
        {
            return _kaynakdurumuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _kaynakdurumuDal.GetCount(filter);
        }

    }
}
