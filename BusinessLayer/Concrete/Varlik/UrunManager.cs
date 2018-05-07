using System.Collections.Generic;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class UrunManager : IUrunService
    {
        IUrunDal _urunDal;

        public UrunManager(IUrunDal urunDal)
        {
            _urunDal = urunDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, UrunRead, UrunLtd")]
        public List<Urun> GetList()
        {
            return _urunDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, UrunRead, UrunLtd")]
        public Urun GetById(int Id)
        {
            return _urunDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikCreate, UrunCreate")]
        public int Add(Urun urun)
        {
            return _urunDal.Add(urun);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, UrunUpdate")]
        public int Update(Urun urun)
        {
            return _urunDal.Update(urun);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, UrunDelete")]
        public int Delete(int Id)
        {
            return _urunDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, UrunDelete")]
        public int DeleteSoft(int Id)
        {
            return _urunDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, UrunRead, UrunLtd")]
        public List<Urun> GetListPagination(PagingParams pagingParams)
        {
            return _urunDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _urunDal.GetCount(filter);
        }
    }
}
