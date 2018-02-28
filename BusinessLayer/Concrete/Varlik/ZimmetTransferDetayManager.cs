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
    public class ZimmetTransferDetayManager : IZimmetTransferDetayService
    {
        IZimmetTransferDetayDal _zimmettransferdetayDal;

        public ZimmetTransferDetayManager(IZimmetTransferDetayDal zimmettransferdetayDal)
        {
            _zimmettransferdetayDal = zimmettransferdetayDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<ZimmetTransferDetay> GetList()
        {
            return _zimmettransferdetayDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<ZimmetTransferDetayDto> GetList(int ZimmetTransferID)
        {
            return _zimmettransferdetayDal.GetList(ZimmetTransferID);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public ZimmetTransferDetay GetById(int Id)
        {
            return _zimmettransferdetayDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(ZimmetTransferDetay zimmettransferdetay)
        {
            return _zimmettransferdetayDal.Add(zimmettransferdetay);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(ZimmetTransferDetay zimmettransferdetay)
        {
            return _zimmettransferdetayDal.Update(zimmettransferdetay);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _zimmettransferdetayDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _zimmettransferdetayDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<ZimmetTransferDetay> GetListPagination(PagingParams pagingParams)
        {
            return _zimmettransferdetayDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _zimmettransferdetayDal.GetCount(filterCol, filterVal);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<ZimmetTransferDetayDto> GetListPaginationDto(int ZimmetTransferID, PagingParams pagingParams)
        {
            return _zimmettransferdetayDal.GetListPaginationDto(ZimmetTransferID, pagingParams);
        }

        public int GetCountDto(int ZimmetTransferID, string filterCol = "", string filterVal = "")
        {
            return _zimmettransferdetayDal.GetCountDto(ZimmetTransferID, filterCol, filterVal);
        }
    }
}