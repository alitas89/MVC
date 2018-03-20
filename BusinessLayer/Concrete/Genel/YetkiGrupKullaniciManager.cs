using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using BusinessLayer.Abstract.Genel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Logging.Log4Net.Layouts;
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

        public int DeleteSoftByKullaniciId(int Id)
        {
            return _yetkigrupkullaniciDal.DeleteSoftByKullaniciId(Id);
        }

        public int AddYetkiGrupKullanici(int kullaniciId, string arrYetkiGrup)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var arr = (Array)jss.DeserializeObject(arrYetkiGrup);

            //kullanıcının tüm yetkileri silinir
            var deleteResult = DeleteSoftByKullaniciId(kullaniciId);
            if (deleteResult >= 0)
            {
                //Her bir yetkigrubu kaydedilir
                foreach (var item in arr)
                {
                    var addResult = Add(new YetkiGrupKullanici()
                    {
                        KullaniciID = kullaniciId,
                        YetkiGrupID = (int)item,
                        Silindi = false
                    });

                    if (addResult < 0)
                    {
                        //Ekleme işlemi sırasında bir hata meydana geldi
                        return -2;
                    }
                }
            }
            else
            {
                //Silme işlemi başarısız
                return -1;
            }
            return 1;
        }
    }
}