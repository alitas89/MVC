using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace BusinessLayer.Concrete.Varlik
{
    public class OzNitelikManager : IOzNitelikService
    {
        IOzNitelikDal _oznitelikDal;
        JavaScriptSerializer jss = new JavaScriptSerializer();

        public OzNitelikManager(IOzNitelikDal oznitelikDal)
        {
            _oznitelikDal = oznitelikDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, OzNitelikRead, OzNitelikLtd")]
        public List<OzNitelik> GetList()
        {
            return _oznitelikDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, OzNitelikRead, OzNitelikLtd")]
        public OzNitelik GetById(int Id)
        {
            return _oznitelikDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikCreate, OzNitelikCreate")]
        public int Add(OzNitelik oznitelik)
        {
            return _oznitelikDal.Add(oznitelik);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, OzNitelikUpdate")]
        public int Update(OzNitelik oznitelik)
        {
            return _oznitelikDal.Update(oznitelik);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, OzNitelikDelete")]
        public int Delete(int Id)
        {
            return _oznitelikDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, OzNitelikDelete")]
        public int DeleteSoft(int Id)
        {
            return _oznitelikDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, OzNitelikRead, OzNitelikLtd")]
        public List<OzNitelik> GetListPagination(PagingParams pagingParams)
        {
            return _oznitelikDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _oznitelikDal.GetCount(filter);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, OzNitelikRead, OzNitelikLtd")]
        public List<OzNitelikDto> GetList(int VarlikSablonID)
        {
            return _oznitelikDal.GetList(VarlikSablonID);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, OzNitelikRead, OzNitelikLtd")]
        public List<OzNitelikDto> GetListByVarlikTuruID(int VarlikTuruID)
        {
            return _oznitelikDal.GetListByVarlikTuruID(VarlikTuruID);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikCreate, OzNitelikCreate")]
        public int AddOzNitelik(int varlikSablonID, string arrOzNitelik)
        {
            var listOzNitelik = JsonConvert.DeserializeObject<List<OzNitelik>>(arrOzNitelik);

            var count = _oznitelikDal.AddWithTransaction(varlikSablonID, listOzNitelik);

             return count;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, OzNitelikUpdate")]
        public int UpdateOzNitelik(int varlikSablonID, string arrOzNitelik)
        {
            var listOzNitelik = JsonConvert.DeserializeObject<List<OzNitelikDto>>(arrOzNitelik);
            // Oz nitelik durumuna göre belirli şablonun nitelikleri güncellenecek yada yeni nitelikler eklenecek

            var count = _oznitelikDal.UpdateWithTransaction(varlikSablonID, listOzNitelik);
            
            return count;
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, OzNitelikRead, OzNitelikLtd")]
        public List<OzNitelikDto> GetListPaginationDto(int VarlikSablonID, PagingParams pagingParams)
        {
            return _oznitelikDal.GetListPaginationDto(VarlikSablonID, pagingParams);
        }

        public int GetCountDto(int VarlikSablonID, string filter = "")
        {
            return _oznitelikDal.GetCountDto(VarlikSablonID, filter);
        }
       
    }
}
