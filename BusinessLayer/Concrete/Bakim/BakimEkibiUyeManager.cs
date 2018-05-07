using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using EntityLayer.Concrete.Personel;
using EntityLayer.Concrete.Varlik;
using Newtonsoft.Json;

namespace BusinessLayer.Concrete.Bakim
{
    public class BakimEkibiUyeManager : IBakimEkibiUyeService
    {
        IBakimEkibiUyeDal _bakimekibiuyeDal;

        public BakimEkibiUyeManager(IBakimEkibiUyeDal bakimekibiuyeDal)
        {
            _bakimekibiuyeDal = bakimekibiuyeDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, BakimEkibiUyeRead, BakimEkibiUyeLtd")]
        public List<BakimEkibiUye> GetList()
        {
            return _bakimekibiuyeDal.GetList();
        }

        [SecuredOperation(Roles = "Admin BakimRead, BakimEkibiUyeRead, BakimEkibiUyeLtd")]
        public BakimEkibiUye GetById(int Id)
        {
            return _bakimekibiuyeDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, BakimEkibiUyeCreate")]
        public int Add(BakimEkibiUye bakimekibiuye)
        {
            return _bakimekibiuyeDal.Add(bakimekibiuye);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, BakimEkibiUyeUpdate")]
        public int Update(BakimEkibiUye bakimekibiuye)
        {
            return _bakimekibiuyeDal.Update(bakimekibiuye);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimEkibiUyeDelete")]
        public int Delete(int Id)
        {
            return _bakimekibiuyeDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimEkibiUyeDelete")]
        public int DeleteSoft(int Id)
        {
            return _bakimekibiuyeDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BakimEkibiUyeRead, BakimEkibiUyeLtd")]
        public List<BakimEkibiUye> GetListPagination(PagingParams pagingParams)
        {
            return _bakimekibiuyeDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _bakimekibiuyeDal.GetCount(filter);
        }

        
        [SecuredOperation(Roles = "Admin, BakimCreate, BakimEkibiUyeCreate")]
        public int AddBakimEkibiUye(int BakimEkibiID, string arrKaynakID)
        {
            var listKaynakID = JsonConvert.DeserializeObject<List<int>>(arrKaynakID);

            var count = _bakimekibiuyeDal.AddWithTransaction(BakimEkibiID, listKaynakID);

            return count;
        }

        private JavaScriptSerializer jss = new JavaScriptSerializer();
        [SecuredOperation(Roles = "Admin, BakimRead, BakimEkibiUyeRead, BakimEkibiUyeLtd")]
        public string GetUyeByBakimEkibiID(int BakimEkibiID)
        {
            var list = _bakimekibiuyeDal.GetListByBakimEkibiID(BakimEkibiID);
            if (list != null && list.Count > 0)
            {
                return jss.Serialize(list.Select(x=>x.KaynakID).ToArray());
            }
            return "";
        }
    }
}