
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
        [SecuredOperation(Roles = "Admin, VarlikRead, ZimmetTransferRead, ZimmetTransferLtd")]
        public List<ZimmetTransfer> GetList()
        {
            return _zimmettransferDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, ZimmetTransferRead, ZimmetTransferLtd")]
        public ZimmetTransfer GetById(int Id)
        {
            return _zimmettransferDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikCreate, ZimmetTransferCreate")]
        public int Add(ZimmetTransfer zimmettransfer)
        {
            return _zimmettransferDal.Add(zimmettransfer);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, ZimmetTransferUpdate")]
        public int Update(ZimmetTransfer zimmettransfer)
        {
            return _zimmettransferDal.Update(zimmettransfer);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, ZimmetTransferDelete")]
        public int Delete(int Id)
        {
            return _zimmettransferDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, ZimmetTransferDelete")]
        public int DeleteSoft(int Id)
        {
            return _zimmettransferDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, ZimmetTransferRead, ZimmetTransferLtd")]
        public List<ZimmetTransfer> GetListPagination(PagingParams pagingParams)
        {
            return _zimmettransferDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _zimmettransferDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, ZimmetTransferRead, ZimmetTransferLtd")]
        public List<ZimmetTransferDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _zimmettransferDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _zimmettransferDal.GetCountDto(filter);
        }
    }
}
