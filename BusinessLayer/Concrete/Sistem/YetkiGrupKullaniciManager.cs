using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using BusinessLayer.Abstract.Sistem;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Concrete.Sistem
{
    public class YetkiGrupKullaniciManager : IYetkiGrupKullaniciService
    {
        IYetkiGrupKullaniciDal _yetkigrupkullaniciDal;
        JavaScriptSerializer jss = new JavaScriptSerializer();

        public YetkiGrupKullaniciManager(IYetkiGrupKullaniciDal yetkigrupkullaniciDal)
        {
            _yetkigrupkullaniciDal = yetkigrupkullaniciDal;
        }

        
        [SecuredOperation(Roles = "Admin, SistemRead, KullaniciRead, KullaniciLtd")]
        public List<YetkiGrupKullanici> GetList()
        {
            return _yetkigrupkullaniciDal.GetList();
        }
        
        public List<YetkiGrupKullanici> GetListByKullaniciId(int kullaniciId)
        {
            return _yetkigrupkullaniciDal.GetListByKullaniciId(kullaniciId);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, KullaniciRead, KullaniciLtd")]
        public YetkiGrupKullanici GetById(int Id)
        {
            return _yetkigrupkullaniciDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, SistemCreate, KullaniciCreate")]
        
        public int Add(YetkiGrupKullanici yetkigrupkullanici)
        {
            return _yetkigrupkullaniciDal.Add(yetkigrupkullanici);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, SistemUpdate, KullaniciUpdate")]
        
        public int Update(YetkiGrupKullanici yetkigrupkullanici)
        {
            return _yetkigrupkullaniciDal.Update(yetkigrupkullanici);
        }

        [SecuredOperation(Roles = "Admin, SistemDelete, KullaniciDelete")]
        
        public int Delete(int Id)
        {
            return _yetkigrupkullaniciDal.Delete(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemDelete, KullaniciDelete")]
        
        public int DeleteSoft(int Id)
        {
            return _yetkigrupkullaniciDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, KullaniciRead, KullaniciLtd")]
        public List<YetkiGrupKullanici> GetListPagination(PagingParams pagingParams)
        {
            return _yetkigrupkullaniciDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _yetkigrupkullaniciDal.GetCount(filter);
        }


        [SecuredOperation(Roles = "Admin, SistemDelete, KullaniciDelete")]
        
        public int DeleteSoftByKullaniciId(int Id)
        {
            return _yetkigrupkullaniciDal.DeleteSoftByKullaniciId(Id);
        }
        
        [SecuredOperation(Roles = "Admin, SistemRead, KullaniciRead, KullaniciLtd")]
        public string GetYetkiGrupListByKullaniciId(int kullaniciId)
        {
            var list = _yetkigrupkullaniciDal.GetListByKullaniciId(kullaniciId);
            if (list != null && list.Count > 0)
            {
                return jss.Serialize(list.Select(x => x.YetkiGrupID).ToArray());
            }
            return "";
        }
        
        [SecuredOperation(Roles = "Admin, SistemCreate, KullaniciCreate")]
        
        public int AddYetkiGrupKullanici(int kullaniciId, string arrYetkiGrup)
        {
            var arrYetki = (Array)jss.DeserializeObject(arrYetkiGrup);

            var count = _yetkigrupkullaniciDal.AddWithTransaction(kullaniciId, arrYetki);

            return count;
        }
    }
}