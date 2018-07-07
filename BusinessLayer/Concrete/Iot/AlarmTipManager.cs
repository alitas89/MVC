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
    public class AlarmTipManager : IAlarmTipService
    {
        IAlarmTipDal _alarmtipDal;

        public AlarmTipManager(IAlarmTipDal alarmtipDal)
        {
            _alarmtipDal = alarmtipDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IOTRead, AlarmTipRead, AlarmTipLtd")]
        public List<AlarmTip> GetList()
        {
            return _alarmtipDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, IOTRead, AlarmTipRead, AlarmTipLtd")]
        public AlarmTip GetById(int Id)
        {
            return _alarmtipDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IOTCreate, AlarmTipCreate")]
        public int Add(AlarmTip alarmtip)
        {
            return _alarmtipDal.Add(alarmtip);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IOTUpdate, AlarmTipUpdate")]
        public int Update(AlarmTip alarmtip)
        {
            return _alarmtipDal.Update(alarmtip);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IOTDelete, AlarmTipDelete")]
        public int Delete(int Id)
        {
            return _alarmtipDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IOTDelete, AlarmTipDelete")]
        public int DeleteSoft(int Id)
        {
            return _alarmtipDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, IOTRead, AlarmTipRead, AlarmTipLtd")]
        public List<AlarmTip> GetListPagination(PagingParams pagingParams)
        {
            return _alarmtipDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _alarmtipDal.GetCount(filter);
        }

    }

}