using System.Collections.Generic;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using EntityLayer.Concrete.Sistem;

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

        [SecuredOperation(Roles = "Admin, BakimRead, PeriyodikBakimRead, PeriyodikBakimLtd, VarlikRead, VarliklarRead")]
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

        [SecuredOperation(Roles = "Admin, BakimUpdate, PeriyodikBakimUpdate")]
        public int UpdateWithTransaction(PeriyodikBakim periyodikBakim, List<int> listBakimPlani, List<int> listBakimRiski, int kullaniciID)
        {
            //Eğer otomatik iş emri oluştur seçildiyse ve bu kişinin o iştipinde iş emri yetkisi yoksa hata döner
            if (periyodikBakim.IsOtomatik)
            {
                List<IsTipiForKullaniciTemp> listIsTipiTemp =
                    _periyodikbakimDal.GetIsTipiListByKullaniciIDForIsEmri(kullaniciID);
                if (!listIsTipiTemp.Exists(x => x.IsTipiID == periyodikBakim.IsTipiID))
                {
                    return -1;
                }
            }

            return _periyodikbakimDal.UpdateWithTransaction(periyodikBakim, listBakimPlani, listBakimRiski, kullaniciID);
        }

        [SecuredOperation(Roles = "Admin, BakimCreate, PeriyodikBakimCreate")]
        public int AddWithTransaction(PeriyodikBakim periyodikBakim, List<int> listBakimPlani, List<int> listBakimRiski, int kullaniciID)
        {
            //Eğer otomatik iş emri oluştur seçildiyse ve bu kişinin o iştipinde iş emri yetkisi yoksa hata döner
            if (periyodikBakim.IsOtomatik)
            {
                List<IsTipiForKullaniciTemp> listIsTipiTemp =
                    _periyodikbakimDal.GetIsTipiListByKullaniciIDForIsEmri(kullaniciID);
                if (!listIsTipiTemp.Exists(x => x.IsTipiID == periyodikBakim.IsTipiID))
                {
                    return -1;
                }
            }

            return _periyodikbakimDal.AddWithTransaction(periyodikBakim, listBakimPlani, listBakimRiski, kullaniciID);
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

        [SecuredOperation(Roles = "Admin, BakimRead, PeriyodikBakimRead, PeriyodikBakimLtd, VarlikRead, VarliklarRead")]
        public List<PeriyodikBakim> GetListByVarlikID(int VarlikID)
        {
            return _periyodikbakimDal.GetListByVarlikID(VarlikID);
        }
        
        [SecuredOperation(Roles = "Admin, BakimDelete, PeriyodikBakimDelete")]
        public int DeleteSoftWithTransaction(int Id)
        {
            int bildirimTriggerID = _periyodikbakimDal.Get(Id).BildirimTriggerID;
            return _periyodikbakimDal.DeleteSoftWithTransaction(Id, bildirimTriggerID);
        }

    }
}