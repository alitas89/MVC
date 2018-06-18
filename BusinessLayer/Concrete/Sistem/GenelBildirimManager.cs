using System.Collections.Generic;
using BusinessLayer.Abstract.Sistem;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Concrete.Sistem
{
    public class GenelBildirimManager : IGenelBildirimService
    {
        IGenelBildirimDal _genelbildirimDal;

        public GenelBildirimManager(IGenelBildirimDal genelbildirimDal)
        {
            _genelbildirimDal = genelbildirimDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Authorized")]
        public List<GenelBildirim> GetList()
        {
            return _genelbildirimDal.GetList();
        }

        [SecuredOperation(Roles = "Authorized")]
        public GenelBildirim GetById(int Id)
        {
            return _genelbildirimDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemCreate, GenelBildirimCreate")]
        public int Add(GenelBildirim genelbildirim)
        {
            return _genelbildirimDal.Add(genelbildirim);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemUpdate, GenelBildirimUpdate")]
        public int Update(GenelBildirim genelbildirim)
        {
            return _genelbildirimDal.Update(genelbildirim);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemDelete, GenelBildirimDelete")]
        public int Delete(int Id)
        {
            return _genelbildirimDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemDelete, GenelBildirimDelete")]
        public int DeleteSoft(int Id)
        {
            return _genelbildirimDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Authorized")]
        public List<GenelBildirim> GetListPagination(PagingParams pagingParams)
        {
            return _genelbildirimDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _genelbildirimDal.GetCount(filter);
        }

        public List<GenelBildirim> GetListYeniBildirimByKime(int Kime)
        {
            return _genelbildirimDal.GetListYeniBildirimByKime(Kime);
        }

        public List<GenelBildirim> GetListByKime(int Kime)
        {
            return _genelbildirimDal.GetListByKime(Kime);
        }
    }

}