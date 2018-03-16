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
    public class YetkiGrupKullaniciManager : IYetkiGrupKullaniciService
    {
        IYetkiGrupKullaniciDal _yetkigrupkullaniciDal;

        public YetkiGrupKullaniciManager(IYetkiGrupKullaniciDal yetkigrupkullaniciDal)
        {
            _yetkigrupkullaniciDal = yetkigrupkullaniciDal;
        }

        public List<YetkiGrupKullanici> GetList()
        {
            return _yetkigrupkullaniciDal.GetList();
        }
        
        public List<YetkiGrupKullanici> GetListByKullaniciId(int kullaniciId)
        {
            return _yetkigrupkullaniciDal.GetListByKullaniciId(kullaniciId);
        }
        
        public YetkiGrupKullanici GetById(int Id)
        {
            return _yetkigrupkullaniciDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        public int Add(YetkiGrupKullanici yetkigrupkullanici)
        {
            return _yetkigrupkullaniciDal.Add(yetkigrupkullanici);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        public int Update(YetkiGrupKullanici yetkigrupkullanici)
        {
            return _yetkigrupkullaniciDal.Update(yetkigrupkullanici);
        }

        public int Delete(int Id)
        {
            return _yetkigrupkullaniciDal.Delete(Id);
        }

        public int DeleteSoft(int Id)
        {
            return _yetkigrupkullaniciDal.DeleteSoft(Id);
        }

        public List<YetkiGrupKullanici> GetListPagination(PagingParams pagingParams)
        {
            return _yetkigrupkullaniciDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _yetkigrupkullaniciDal.GetCount(filter);
        }

    }
}