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
    public class IsTalebiOnayBirimManager : IIsTalebiOnayBirimService
    {
        IIsTalebiOnayBirimDal _ıstalebionaybirimDal;

        public IsTalebiOnayBirimManager(IIsTalebiOnayBirimDal ıstalebionaybirimDal)
        {
            _ıstalebionaybirimDal = ıstalebionaybirimDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiOnayBirimRead, IsTalebiOnayBirimLtd")]
        public List<IsTalebiOnayBirim> GetList()
        {
            return _ıstalebionaybirimDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiOnayBirimRead, IsTalebiOnayBirimLtd")]
        public IsTalebiOnayBirim GetById(int Id)
        {
            return _ıstalebionaybirimDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemCreate, IsTalebiOnayBirimCreate")]
        public int Add(IsTalebiOnayBirim ıstalebionaybirim)
        {
            return _ıstalebionaybirimDal.Add(ıstalebionaybirim);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemUpdate, IsTalebiOnayBirimUpdate")]
        public int Update(IsTalebiOnayBirim ıstalebionaybirim)
        {
            return _ıstalebionaybirimDal.Update(ıstalebionaybirim);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemDelete, IsTalebiOnayBirimDelete")]
        public int Delete(int Id)
        {
            return _ıstalebionaybirimDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemDelete, IsTalebiOnayBirimDelete")]
        public int DeleteSoft(int Id)
        {
            return _ıstalebionaybirimDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiOnayBirimRead, IsTalebiOnayBirimLtd")]
        public List<IsTalebiOnayBirim> GetListPagination(PagingParams pagingParams)
        {
            return _ıstalebionaybirimDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _ıstalebionaybirimDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, IsTalebiOnayBirimRead, IsTalebiOnayBirimLtd")]
        public List<IsTalebiKullaniciTemp> GetListByIsTipiID(int IsTipiID)
        {
            return _ıstalebionaybirimDal.GetListByIsTipiID(IsTipiID);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemCreate, IsTalebiOnayBirimCreate")]
        public int AddIsTalebiOnayBirim(int IsTipiID, string arrKullaniciID)
        {
            var listKullaniciID = JsonConvert.DeserializeObject<List<int>>(arrKullaniciID);

            var count = _ıstalebionaybirimDal.AddWithTransaction(IsTipiID, listKullaniciID);

            return count;
        }
    }

}