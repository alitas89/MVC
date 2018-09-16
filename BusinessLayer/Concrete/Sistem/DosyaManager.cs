using BusinessLayer.Abstract.Sistem;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.Sistem
{
    public class DosyaManager : IDosyaService
    {
        IDosyaDal _dosyaDal;

        public DosyaManager(IDosyaDal dosyaDal)
        {
            _dosyaDal = dosyaDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, DosyaRead, DosyaLtd")]
        public List<Dosya> GetList()
        {
            return _dosyaDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, DosyaRead, DosyaLtd")]
        public Dosya GetById(int Id)
        {
            return _dosyaDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, DosyaCreate")]
        public int Add(Dosya dosya)
        {
            return _dosyaDal.Add(dosya);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, DosyaUpdate")]
        public int Update(Dosya dosya)
        {
            return _dosyaDal.Update(dosya);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, DosyaDelete")]
        public int Delete(int Id)
        {
            return _dosyaDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, DosyaDelete")]
        public int DeleteSoft(int Id)
        {
            return _dosyaDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, DosyaRead, DosyaLtd")]
        public List<Dosya> GetListPagination(PagingParams pagingParams)
        {
            return _dosyaDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _dosyaDal.GetCount(filter);
        }

        public List<Dosya> GetListByBagliID(int id)
        {
            return _dosyaDal.GetListByBagliID(id);
        }
    }

}
