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
    public class VarlikTransferManager : IVarlikTransferService
    {
        IVarlikTransferDal _varliktransferDal;

        public VarlikTransferManager(IVarlikTransferDal varliktransferDal)
        {
            _varliktransferDal = varliktransferDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public List<VarlikTransfer> GetList()
        {
            return _varliktransferDal.GetList();
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public VarlikTransfer GetById(int Id)
        {
            return _varliktransferDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Add(VarlikTransfer varliktransfer)
        {
            //Varlık kısım ve bağlıVarlıkKod güncellenmelidir.
            int updateResult = _varliktransferDal.UpdateVarlikKisimBagliVarlikKod(varliktransfer.VarlikID, varliktransfer.YeniKisimID, varliktransfer.YeniSahipVarlikID);
            //Ekleme yapılır
            return updateResult > 0 ? _varliktransferDal.Add(varliktransfer) : 0;
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Update(VarlikTransfer varliktransfer)
        {
            //Varlık kısım ve bağlıVarlıkKod güncellenmelidir.
            int updateResult = _varliktransferDal.UpdateVarlikKisimBagliVarlikKod(varliktransfer.VarlikID, varliktransfer.YeniKisimID, varliktransfer.YeniSahipVarlikID);
            //Ekleme yapılır
            return updateResult > 0 ? _varliktransferDal.Update(varliktransfer) : 0;
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int Delete(int Id)
        {
            return _varliktransferDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin,Editor")]
        public int DeleteSoft(int Id)
        {
            return _varliktransferDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<VarlikTransfer> GetListPagination(PagingParams pagingParams)
        {
            return _varliktransferDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            return _varliktransferDal.GetCount(filterCol, filterVal);
        }

        [SecuredOperation(Roles = "Admin,Editor")]
        public List<VarlikTransferDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _varliktransferDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filterCol = "", string filterVal = "")
        {
            return _varliktransferDal.GetCountDto(filterCol, filterVal);
        }
    }
}