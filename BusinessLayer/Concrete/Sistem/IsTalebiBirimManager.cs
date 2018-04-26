using System.Collections.Generic;
using BusinessLayer.Abstract.Sistem;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;
using Newtonsoft.Json;

namespace BusinessLayer.Concrete.Sistem
{
     public class IsTalebiBirimManager : IIsTalebiBirimService
    {
        IIsTalebiBirimDal _ıstalebibirimDal;

        public IsTalebiBirimManager(IIsTalebiBirimDal ıstalebibirimDal)
        {
            _ıstalebibirimDal = ıstalebibirimDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiBirimRead, IsTalebiBirimLtd")]
        public List<IsTalebiBirim> GetList()
        {
            return _ıstalebibirimDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiBirimRead, IsTalebiBirimLtd")]
        public IsTalebiBirim GetById(int Id)
        {
            return _ıstalebibirimDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemCreate, IsTalebiBirimCreate")]
        public int Add(IsTalebiBirim ıstalebibirim)
        {
            return _ıstalebibirimDal.Add(ıstalebibirim);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemUpdate, IsTalebiBirimUpdate")]
        public int Update(IsTalebiBirim ıstalebibirim)
        {
            return _ıstalebibirimDal.Update(ıstalebibirim);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemDelete, IsTalebiBirimDelete")]
        public int Delete(int Id)
        {
            return _ıstalebibirimDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemDelete, IsTalebiBirimDelete")]
        public int DeleteSoft(int Id)
        {
            return _ıstalebibirimDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiBirimRead, IsTalebiBirimLtd")]
        public List<IsTalebiBirim> GetListPagination(PagingParams pagingParams)
        {
            return _ıstalebibirimDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _ıstalebibirimDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiBirimRead, IsTalebiBirimLtd")]
        public List<IsTalebiKullaniciTemp> GetListByIsTipiID(int IsTipiID)
        {
            return _ıstalebibirimDal.GetListByIsTipiID(IsTipiID);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemCreate, IsTalebiBirimCreate")]
        public int AddIsTalebiBirim(int IsTipiID, string arrKullaniciID)
        {
            var listKullaniciID = JsonConvert.DeserializeObject<List<int>>(arrKullaniciID);

            var count = _ıstalebibirimDal.AddWithTransaction(IsTipiID, listKullaniciID);

            return count;
        }
    }

}