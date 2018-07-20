using System.Collections.Generic;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class BakimPlaniManager : IBakimPlaniService
    {
        IBakimPlaniDal _bakimplaniDal;

        public BakimPlaniManager(IBakimPlaniDal bakimplaniDal)
        {
            _bakimplaniDal = bakimplaniDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimRead, BakimPlaniRead, BakimPlaniLtd")]
        public List<BakimPlani> GetList()
        {
            return _bakimplaniDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BakimPlaniRead, BakimPlaniLtd")]
        public BakimPlani GetById(int Id)
        {
            return _bakimplaniDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimCreate, BakimPlaniCreate")]
        public int Add(BakimPlani bakimplani)
        {
            return _bakimplaniDal.Add(bakimplani);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimUpdate, BakimPlaniUpdate")]
        public int Update(BakimPlani bakimplani)
        {
            return _bakimplaniDal.Update(bakimplani);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimPlaniDelete")]
        public int Delete(int Id)
        {
            return _bakimplaniDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimPlaniDelete")]
        public int DeleteSoft(int Id)
        {
            return _bakimplaniDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BakimPlaniRead, BakimPlaniLtd")]
        public List<BakimPlani> GetListPagination(PagingParams pagingParams)
        {
            return _bakimplaniDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _bakimplaniDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, BakimCreate, BakimPlaniCreate")]
        public int AddWithTransaction(BakimPlani bakimplani, List<IsAdimlari> listIsAdimlari)
        {
            return _bakimplaniDal.AddWithTransaction(bakimplani, listIsAdimlari);
        }

        [SecuredOperation(Roles = "Admin, BakimUpdate, BakimPlaniUpdate")]
        public int UpdateWithTransaction(BakimPlani bakimplani, List<IsAdimlari> listIsAdimlari)
        {
            foreach (var isAdimlari in listIsAdimlari)
            {
                isAdimlari.BakimPlaniID = bakimplani.BakimPlaniID;
                isAdimlari.Silindi = false;
            }
            return _bakimplaniDal.UpdateWithTransaction(bakimplani, listIsAdimlari);
        }

        public List<BakimPlani> GetListBakimPlaniByPeriyodikBakimID(int PeriyodikBakimID)
        {
            return _bakimplaniDal.GetListBakimPlaniByPeriyodikBakimID(PeriyodikBakimID);
        }

        public List<string> AddListWithTransactionBySablon(List<BakimPlani> listBakimPlani)
        {
            return _bakimplaniDal.AddListWithTransactionBySablon(listBakimPlani);
        }
    }

}