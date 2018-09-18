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
        
        [SecuredOperation(Roles = "Authorized")]
        public List<Dosya> GetList()
        {
            return _dosyaDal.GetList();
        }

        [SecuredOperation(Roles = "Authorized")]
        public Dosya GetById(int Id)
        {
            return _dosyaDal.Get(Id);
        }

        //[SecuredOperation(Roles = "Authorized, VarlikCreate, VarliklarCreate, VarlikUpdate, VarliklarUpdate")]
        public int Add(Dosya dosya)
        {
            return _dosyaDal.Add(dosya);
        }

        [SecuredOperation(Roles = "Authorized")]
        public int Update(Dosya dosya)
        {
            return _dosyaDal.Update(dosya);
        }
        
        [SecuredOperation(Roles = "Authorized, VarlikCreate, VarliklarCreate, VarlikUpdate, VarliklarUpdate")]
        public int Delete(int Id)
        {
            return _dosyaDal.Delete(Id);
        }
        
        //[SecuredOperation(Roles = "Authorized, VarlikCreate, VarliklarCreate, VarlikUpdate, VarliklarUpdate")]
        public int DeleteSoft(int Id)
        {
            return _dosyaDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Authorized")]
        public List<Dosya> GetListPagination(PagingParams pagingParams)
        {
            return _dosyaDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _dosyaDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Authorized")]
        public List<Dosya> GetListByBagliID(int id)
        {
            return _dosyaDal.GetListByBagliID(id);
        }
    }

}
