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
    public class YetkiGrupRolManager : IYetkiGrupRolService
    {
        IYetkiGrupRolDal _yetkigruprolDal;
        JavaScriptSerializer jss = new JavaScriptSerializer();

        public YetkiGrupRolManager(IYetkiGrupRolDal yetkigruprolDal)
        {
            _yetkigruprolDal = yetkigruprolDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemRead, YetkiGrupRead, YetkiGrupLtd")]
        public List<YetkiGrupRol> GetList()
        {
            return _yetkigruprolDal.GetList();
        }
        
        [SecuredOperation(Roles = "Admin, SistemRead, YetkiGrupRead, YetkiGrupLtd")]
        public YetkiGrupRol GetById(int Id)
        {
            return _yetkigruprolDal.Get(Id);
        }
        
        [SecuredOperation(Roles = "Admin, SistemCreate, YetkiGrupCreate")]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int Add(YetkiGrupRol yetkigruprol)
        {
            return _yetkigruprolDal.Add(yetkigruprol);
        }
        
        [SecuredOperation(Roles = "Admin, SistemUpdate, YetkiGrupUpdate")]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int Update(YetkiGrupRol yetkigruprol)
        {
            return _yetkigruprolDal.Update(yetkigruprol);
        }

        [SecuredOperation(Roles = "Admin, SistemDelete, YetkiGrupDelete")]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int Delete(int Id)
        {
            return _yetkigruprolDal.Delete(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemDelete, YetkiGrupDelete")]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int DeleteSoft(int Id)
        {
            return _yetkigruprolDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, YetkiGrupRead, YetkiGrupLtd")]
        public List<YetkiGrupRol> GetListPagination(PagingParams pagingParams)
        {
            return _yetkigruprolDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _yetkigruprolDal.GetCount(filter);
        }
        
        public List<YetkiGrupRol> GetListByGrupId(int YetkiGrupID)
        {
            return _yetkigruprolDal.GetListByGrupId(YetkiGrupID);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, YetkiGrupRead, YetkiGrupLtd")]
        public string GetYetkiRolByYetkiGrupID(int YetkiGrupID)
        {
            var list = _yetkigruprolDal.GetListByGrupId(YetkiGrupID);
            if (list != null && list.Count > 0)
            {
                return jss.Serialize(list.Select(x => x.YetkiRolKod).ToArray());
            }
            return "";
        }

        [SecuredOperation(Roles = "Admin, SistemDelete, YetkiGrupDelete")]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int DeleteSoftByYetkiGrupID(int YetkiGrupID)
        {
            return _yetkigruprolDal.DeleteSoftByYetkiGrupID(YetkiGrupID);
        }

        [SecuredOperation(Roles = "Admin, SistemCreate, YetkiGrupCreate")]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int AddYetkiGrupRoles(int yetkiGrupID, string arrYetkiRol)
        {
            var arr = (Array)jss.DeserializeObject(arrYetkiRol);

            //grubun tüm rolleri silinir
            var deleteResult = DeleteSoftByYetkiGrupID(yetkiGrupID);
            if (deleteResult >= 0)
            {
                //Her bir rol kaydedilir kaydedilir
                foreach (var item in arr)
                {
                    //Gelen arr , ile ayrılmış bir dizi olabilir
                    string strComma = item.ToString();
                    if (strComma.Contains(','))
                    {
                        var arrComma = item.ToString().Split(',');
                        foreach (var role in arrComma)
                        {
                            var addResultComma = Add(new YetkiGrupRol()
                            {
                                YetkiGrupID = yetkiGrupID,
                                YetkiRolKod = role.Trim(),
                                Silindi = false
                            });

                            if (addResultComma < 0)
                            {
                                //Ekleme işlemi sırasında bir hata meydana geldi
                                return -2;
                            }
                        }
                    }
                    else
                    {
                        var addResult = Add(new YetkiGrupRol()
                        {
                            YetkiGrupID = yetkiGrupID,
                            YetkiRolKod = item.ToString().Trim(),
                            Silindi = false
                        });

                        if (addResult < 0)
                        {
                            //Ekleme işlemi sırasında bir hata meydana geldi
                            return -2;
                        }
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