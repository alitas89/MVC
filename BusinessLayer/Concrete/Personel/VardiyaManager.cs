using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Personel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.DtoModel.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Concrete.Personel
{
    public class VardiyaManager : IVardiyaService
    {
        IVardiyaDal _vardiyaDal;

        public VardiyaManager(IVardiyaDal vardiyaDal)
        {
            _vardiyaDal = vardiyaDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<Vardiya> GetList()
        {
            return _vardiyaDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public Vardiya GetById(int Id)
        {
            return _vardiyaDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(Vardiya vardiya)
        {
            return _vardiyaDal.Add(vardiya);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(Vardiya vardiya)
        {
            return _vardiyaDal.Update(vardiya);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _vardiyaDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _vardiyaDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<Vardiya> GetListPagination(PagingParams pagingParams)
        {
            return _vardiyaDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _vardiyaDal.GetCount(filterCol, filterVal);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<VardiyaDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _vardiyaDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filterCol = "", string filterVal = "")
        {
            return _vardiyaDal.GetCountDto(filterCol, filterVal);
        }

    }
}
