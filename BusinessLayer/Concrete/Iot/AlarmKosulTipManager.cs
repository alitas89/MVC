using System.Collections.Generic;
using BusinessLayer.Abstract.Iot;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;

namespace BusinessLayer.Concrete.Iot
{
    public class AlarmKosulTipManager : IAlarmKosulTipService
    {
        IAlarmKosulTipDal _alarmkosultipDal;

        public AlarmKosulTipManager(IAlarmKosulTipDal alarmkosultipDal)
        {
            _alarmkosultipDal = alarmkosultipDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AlarmKosulTipRead, AlarmKosulTipLtd")]
        public List<AlarmKosulTip> GetList()
        {
            return _alarmkosultipDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, AlarmKosulTipRead, AlarmKosulTipLtd")]
        public AlarmKosulTip GetById(int Id)
        {
            return _alarmkosultipDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AlarmKosulTipCreate")]
        public int Add(AlarmKosulTip alarmkosultip)
        {
            return _alarmkosultipDal.Add(alarmkosultip);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AlarmKosulTipUpdate")]
        public int Update(AlarmKosulTip alarmkosultip)
        {
            return _alarmkosultipDal.Update(alarmkosultip);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AlarmKosulTipDelete")]
        public int Delete(int Id)
        {
            return _alarmkosultipDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AlarmKosulTipDelete")]
        public int DeleteSoft(int Id)
        {
            return _alarmkosultipDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, AlarmKosulTipRead, AlarmKosulTipLtd")]
        public List<AlarmKosulTip> GetListPagination(PagingParams pagingParams)
        {
            return _alarmkosultipDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _alarmkosultipDal.GetCount(filter);
        }

    }

}