using BusinessLayer.Abstract.Personel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Varlik;

namespace BusinessLayer.Concrete.Personel
{
    public class KaynakManager : IKaynakService
    {
        IKaynakDal _kaynakDal;

        public KaynakManager(IKaynakDal kaynakDal)
        {
            _kaynakDal = kaynakDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<Kaynak> GetList()
        {
            return _kaynakDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public Kaynak GetById(int Id)
        {
            return _kaynakDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(Kaynak kaynak)
        {
            return _kaynakDal.Add(kaynak);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(Kaynak kaynak)
        {
            return _kaynakDal.Update(kaynak);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _kaynakDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _kaynakDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<Kaynak> GetListPagination(PagingParams pagingParams)
        {
            return _kaynakDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _kaynakDal.GetCount(filterCol, filterVal);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<KaynakDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _kaynakDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filterCol = "", string filterVal = "")
        {
            return _kaynakDal.GetCountDto(filterCol, filterVal);
        }
    }
}
