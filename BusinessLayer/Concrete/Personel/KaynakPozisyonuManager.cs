using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Personel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Concrete.Personel
{
    public class KaynakPozisyonuManager : IKaynakPozisyonuService
    {
        IKaynakPozisyonuDal _kaynakpozisyonuDal;

        public KaynakPozisyonuManager(IKaynakPozisyonuDal kaynakpozisyonuDal)
        {
            _kaynakpozisyonuDal = kaynakpozisyonuDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<KaynakPozisyonu> GetList()
        {
            return _kaynakpozisyonuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public KaynakPozisyonu GetById(int Id)
        {
            return _kaynakpozisyonuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(KaynakPozisyonu kaynakpozisyonu)
        {
            return _kaynakpozisyonuDal.Add(kaynakpozisyonu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(KaynakPozisyonu kaynakpozisyonu)
        {
            return _kaynakpozisyonuDal.Update(kaynakpozisyonu);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _kaynakpozisyonuDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _kaynakpozisyonuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<KaynakPozisyonu> GetListPagination(PagingParams pagingParams)
        {
            return _kaynakpozisyonuDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _kaynakpozisyonuDal.GetCount(filterCol, filterVal);
        }

    }
}
