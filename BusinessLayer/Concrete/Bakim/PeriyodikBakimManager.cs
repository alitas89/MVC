using System.Collections.Generic;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class PeriyodikBakimManager : IPeriyodikBakimService
    {
        IPeriyodikBakimDal _periyodikbakimDal;

        public PeriyodikBakimManager(IPeriyodikBakimDal periyodikbakimDal)
        {
            _periyodikbakimDal = periyodikbakimDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimRead, PeriyodikBakimRead, PeriyodikBakimLtd")]
        public List<PeriyodikBakim> GetList()
        {
            return _periyodikbakimDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, PeriyodikBakimRead, PeriyodikBakimLtd")]
        public PeriyodikBakim GetById(int Id)
        {
            return _periyodikbakimDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimCreate, PeriyodikBakimCreate")]
        public int Add(PeriyodikBakim periyodikbakim)
        {
            return _periyodikbakimDal.Add(periyodikbakim);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimUpdate, PeriyodikBakimUpdate")]
        public int Update(PeriyodikBakim periyodikbakim)
        {
            return _periyodikbakimDal.Update(periyodikbakim);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDelete, PeriyodikBakimDelete")]
        public int Delete(int Id)
        {
            return _periyodikbakimDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDelete, PeriyodikBakimDelete")]
        public int DeleteSoft(int Id)
        {
            return _periyodikbakimDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, PeriyodikBakimRead, PeriyodikBakimLtd")]
        public List<PeriyodikBakim> GetListPagination(PagingParams pagingParams)
        {
            return _periyodikbakimDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _periyodikbakimDal.GetCount(filter);
        }

        public int UpdateWithTransaction(PeriyodikBakim periyodikBakim, List<int> listBakimPlani, List<int> listBakimRiski)
        {
            return _periyodikbakimDal.UpdateWithTransaction(periyodikBakim, listBakimPlani, listBakimRiski);
        }

        public int AddWithTransaction(PeriyodikBakim periyodikBakim, List<int> listBakimPlani, List<int> listBakimRiski)
        {
            return _periyodikbakimDal.AddWithTransaction(periyodikBakim, listBakimPlani, listBakimRiski);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, PeriyodikBakimRead, PeriyodikBakimLtd")]
        public List<PeriyodikBakimDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _periyodikbakimDal.GetListPaginationDto(pagingParams);
        }


        public int GetCountDto(string filter = "")
        {
            return _periyodikbakimDal.GetCountDto(filter);
        }
    }

}