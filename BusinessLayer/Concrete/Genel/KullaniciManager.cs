using System.Collections.Generic;
using BusinessLayer.Abstract.Genel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Concrete.Genel
{
    public class KullaniciManager : IKullaniciService
    {
        IKullaniciDal _kullaniciDal;

        public KullaniciManager(IKullaniciDal kullaniciDal)
        {
            _kullaniciDal = kullaniciDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemRead, KullaniciRead, KullaniciLtd")]
        public List<Kullanici> GetList()
        {
            return _kullaniciDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SistemRead, KullaniciRead, KullaniciLtd")]
        public Kullanici GetById(int Id)
        {
            return _kullaniciDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemCreate, KullaniciCreate")]
        public int Add(Kullanici kullanici)
        {
            //KullaniciAdi kontrol edilir - benzersiz olmalıdır
            var item = _kullaniciDal.GetList().Find(x => x.KullaniciAdi == kullanici.KullaniciAdi);
            if (item != null)
            {
                return 0;
            }
            return _kullaniciDal.Add(kullanici);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemUpdate, KullaniciUpdate")]
        public int Update(Kullanici kullanici)
        {
            var item = _kullaniciDal.GetList().Find(x => x.KullaniciAdi == kullanici.KullaniciAdi);
            if (item != null)
            {
                //KullanıcıAdı var aynı id mi?
                if (item.KullaniciId != kullanici.KullaniciId)
                {
                    return 0;
                }
            }
            return _kullaniciDal.Update(kullanici);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemDelete, KullaniciDelete")]
        public int Delete(int Id)
        {
            return _kullaniciDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemDelete, KullaniciDelete")]
        public int DeleteSoft(int Id)
        {
            return _kullaniciDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, KullaniciRead, KullaniciLtd")]
        public List<Kullanici> GetListPagination(PagingParams pagingParams)
        {
            return _kullaniciDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _kullaniciDal.GetCount(filter);
        }

        public Kullanici GetByKullaniciAdiAndSifre(string kullaniciAdi, string sifre)
        {
            return _kullaniciDal.GetByKullaniciAdiAndSifre(kullaniciAdi, sifre);
        }
    }
}

