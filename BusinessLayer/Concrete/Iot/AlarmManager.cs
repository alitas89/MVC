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
    public class AlarmManager : IAlarmService
    {
        IAlarmDal _alarmDal;

        public AlarmManager(IAlarmDal alarmDal)
        {
            _alarmDal = alarmDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AlarmRead, AlarmLtd")]
        public List<Alarm> GetList()
        {
            return _alarmDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, AlarmRead, AlarmLtd")]
        public Alarm GetById(int Id)
        {
            return _alarmDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AlarmCreate")]
        public int Add(Alarm alarm)
        {
            return _alarmDal.Add(alarm);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AlarmUpdate")]
        public int Update(Alarm alarm)
        {
            return _alarmDal.Update(alarm);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AlarmDelete")]
        public int Delete(int Id)
        {
            return _alarmDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, AlarmDelete")]
        public int DeleteSoft(int Id)
        {
            return _alarmDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, AlarmRead, AlarmLtd")]
        public List<Alarm> GetListPagination(PagingParams pagingParams)
        {
            return _alarmDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _alarmDal.GetCount(filter);
        }

    }
}