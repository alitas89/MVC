using System.Collections.Generic;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using Newtonsoft.Json;

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
        [SecuredOperation(Roles = "Admin, VarlikRead, ZimmetTransferDetayRead, ZimmetTransferDetayLtd")]
        public List<ZimmetTransferDetay> GetList()
        {
            return _zimmettransferdetayDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikRead, ZimmetTransferDetayRead, ZimmetTransferDetayLtd")]
        public List<ZimmetTransferDetayDto> GetList(int ZimmetTransferID)
        {
            return _zimmettransferdetayDal.GetList(ZimmetTransferID);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, ZimmetTransferDetayRead, ZimmetTransferDetayLtd")]
        public ZimmetTransferDetay GetById(int Id)
        {
            return _zimmettransferdetayDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikAdd, ZimmetTransferDetayCreate")]
        public int Add(ZimmetTransferDetay zimmettransferdetay)
        {
            //Kişi bilgisi alınır
            int zimmetAlanID = _zimmettransferdetayDal.GetZimmetliPersonel(zimmettransferdetay.ZimmetTransferID);
            //Varlık zimmeti güncellenmelidir.
            int updateResult = _zimmettransferdetayDal.UpdateVarlikZimmet(zimmettransferdetay.VarlikID, zimmetAlanID);
            //Ekleme yapılır
            return updateResult > 0 ? _zimmettransferdetayDal.Add(zimmettransferdetay) : 0;
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, ZimmetTransferDetayUpdate")]
        public int Update(ZimmetTransferDetay zimmettransferdetay)
        {
            //Kişi bilgisi alınır
            int zimmetAlanID = _zimmettransferdetayDal.GetZimmetliPersonel(zimmettransferdetay.ZimmetTransferID);
            //Varlık zimmeti güncellenmelidir.
            int updateResult = _zimmettransferdetayDal.UpdateVarlikZimmet(zimmettransferdetay.VarlikID, zimmetAlanID);
            //Güncelleme yapılır
            return updateResult > 0 ? _zimmettransferdetayDal.Update(zimmettransferdetay) : 0;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, ZimmetTransferDetayDelete")]
        public int Delete(int Id)
        {
            return _zimmettransferdetayDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, ZimmetTransferDetayDelete")]
        public int DeleteSoft(int Id)
        {
            return _zimmettransferdetayDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, ZimmetTransferDetayRead, ZimmetTransferDetayLtd")]
        public List<ZimmetTransferDetay> GetListPagination(PagingParams pagingParams)
        {
            return _zimmettransferdetayDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _zimmettransferdetayDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, ZimmetTransferDetayRead, ZimmetTransferDetayLtd")]
        public List<ZimmetTransferDetayDto> GetListPaginationDto(int ZimmetTransferID, PagingParams pagingParams)
        {
            return _zimmettransferdetayDal.GetListPaginationDto(ZimmetTransferID, pagingParams);
        }

        public int GetCountDto(int ZimmetTransferID, string filter = "")
        {
            return _zimmettransferdetayDal.GetCountDto(ZimmetTransferID, filter);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikCreate, ZimmetTransferDetayCreate")]
        public int AddZimmetTransferDetay(int ZimmetTransferID, string arrVarlik)
        {
            var listZimmetTransferDetay = JsonConvert.DeserializeObject<List<ZimmetTransferDetay>>(arrVarlik);

            var count = _zimmettransferdetayDal.AddWithTransaction(ZimmetTransferID, listZimmetTransferDetay);

            return count;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, ZimmetTransferDetayUpdate")]
        public int UpdateZimmetTransferDetay(int ZimmetTransferID, string arrVarlik)
        {
            var listZimmetTransferDetay = JsonConvert.DeserializeObject<List<ZimmetTransferDetayDto>>(arrVarlik);

            var count = _zimmettransferdetayDal.UpdateWithTransaction(ZimmetTransferID, listZimmetTransferDetay);

            return count;
        }
    }
}