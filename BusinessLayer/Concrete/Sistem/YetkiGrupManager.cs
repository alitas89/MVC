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
    public class YetkiGrupManager : IYetkiGrupService
    {
        IYetkiGrupDal _yetkigrupDal;

        public YetkiGrupManager(IYetkiGrupDal yetkigrupDal)
        {
            _yetkigrupDal = yetkigrupDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemRead, YetkiGrupRead, YetkiGrupLtd")]
        public List<YetkiGrup> GetList()
        {
            return _yetkigrupDal.GetList();
        }
        
        [SecuredOperation(Roles = "Admin, SistemRead, YetkiGrupRead, YetkiGrupLtd")]
        public YetkiGrup GetById(int Id)
        {
            return _yetkigrupDal.Get(Id);
        }
        
        [SecuredOperation(Roles = "Admin, SistemCreate, YetkiGrupCreate")]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int Add(YetkiGrup yetkigrup)
        {
            //YetkiKodu kontrol edilir - benzersiz olmalıdır
            var item = _yetkigrupDal.GetList().Find(x => x.Kod == yetkigrup.Kod);
            if (item != null)
            {
                return 0;
            }
            return _yetkigrupDal.Add(yetkigrup);
        }

        [SecuredOperation(Roles = "Admin, SistemUpdate, YetkiGrupUpdate")]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int Update(YetkiGrup yetkigrup)
        {
            //74-İşTalep ve 75-İşEmri kayıtları silinemez!
            if (yetkigrup.YetkiGrupID==74 || yetkigrup.YetkiGrupID==75)
            {
                //İşTalep ve İşEmri yetki grupları silinemez!
                return -1;
            }

            var item = _yetkigrupDal.GetList().Find(x => x.Kod == yetkigrup.Kod);
            if (item != null)
            {
                //KullanıcıAdı var aynı id mi?
                if (item.YetkiGrupID != yetkigrup.YetkiGrupID)
                {
                    return 0;
                }
            }
            return _yetkigrupDal.Update(yetkigrup);
        }

        [SecuredOperation(Roles = "Admin, SistemUpdate, YetkiGrupDelete")]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int Delete(int Id)
        {
            return _yetkigrupDal.Delete(Id);
        }


        [SecuredOperation(Roles = "Admin, SistemUpdate, YetkiGrupDelete")]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int DeleteSoft(int Id)
        {
            YetkiGrup yetkigrup = _yetkigrupDal.Get(Id);

            //74-İşTalep ve 75-İşEmri kayıtları silinemez!
            if (yetkigrup.YetkiGrupID == 74 || yetkigrup.YetkiGrupID == 75)
            {
                //İşTalep ve İşEmri yetki grupları silinemez!
                return -1;
            }

            return _yetkigrupDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, YetkiGrupRead, YetkiGrupLtd")]
        public List<YetkiGrup> GetListPagination(PagingParams pagingParams)
        {
            return _yetkigrupDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _yetkigrupDal.GetCount(filter);
        }

    }
}