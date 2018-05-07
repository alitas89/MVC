using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Malzeme;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Concrete.Malzeme
{
    public class OlcuBirimManager : IOlcuBirimService
    {
        IOlcuBirimDal _olcubirimDal;

        public OlcuBirimManager(IOlcuBirimDal olcubirimDal)
        {
            _olcubirimDal = olcubirimDal;
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeRead, OlcuBirimRead, OlcuBirimLtd")]
        public List<OlcuBirim> GetList()
        {
            return _olcubirimDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, OlcuBirimRead, OlcuBirimLtd")]
        public OlcuBirim GetById(int Id)
        {
            return _olcubirimDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeCreate, OlcuBirimCreate")]
        public int Add(OlcuBirim olcubirim)
        {
            return _olcubirimDal.Add(olcubirim);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeUpdate, OlcuBirimUpdate")]
        public int Update(OlcuBirim olcubirim)
        {
            return _olcubirimDal.Update(olcubirim);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, OlcuBirimDelete")]
        public int Delete(int Id)
        {
            return _olcubirimDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, OlcuBirimDelete")]
        public int DeleteSoft(int Id)
        {
            return _olcubirimDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, OlcuBirimRead, OlcuBirimLtd")]
        public List<OlcuBirim> GetListPagination(PagingParams pagingParams)
        {
            return _olcubirimDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _olcubirimDal.GetCount(filter);
        }

    }
}
