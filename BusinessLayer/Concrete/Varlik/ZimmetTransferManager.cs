
using System.Collections.Generic;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class ZimmetTransferManager : IZimmetTransferService
    {
        IZimmetTransferDal _zimmettransferDal;

        public ZimmetTransferManager(IZimmetTransferDal zimmettransferDal)
        {
            _zimmettransferDal = zimmettransferDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<ZimmetTransfer> GetList()
        {
            return _zimmettransferDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public ZimmetTransfer GetById(int Id)
        {
            return _zimmettransferDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(ZimmetTransfer zimmettransfer)
        {
            return _zimmettransferDal.Add(zimmettransfer);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(ZimmetTransfer zimmettransfer)
        {
            return _zimmettransferDal.Update(zimmettransfer);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _zimmettransferDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _zimmettransferDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<ZimmetTransfer> GetListPagination(PagingParams pagingParams)
        {
            return _zimmettransferDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _zimmettransferDal.GetCount(filterCol, filterVal);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<ZimmetTransferDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _zimmettransferDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filterCol = "", string filterVal = "")
        {
            return _zimmettransferDal.GetCountDto(filterCol, filterVal);
        }
    }
}
