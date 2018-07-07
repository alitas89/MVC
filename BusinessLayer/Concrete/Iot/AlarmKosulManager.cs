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
    public class AlarmKosulManager : IAlarmKosulService
    {
        IAlarmKosulDal _alarmkosulDal;

        public AlarmKosulManager(IAlarmKosulDal alarmkosulDal)
        {
            _alarmkosulDal = alarmkosulDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IOTRead, AlarmKosulRead, AlarmKosulLtd")]
        public List<AlarmKosul> GetList()
        {
            return _alarmkosulDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, IOTRead, AlarmKosulRead, AlarmKosulLtd")]
        public AlarmKosul GetById(int Id)
        {
            return _alarmkosulDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IOTCreate,  AlarmKosulCreate")]
        public int Add(AlarmKosul alarmkosul)
        {
            return _alarmkosulDal.Add(alarmkosul);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IOTUpdate, AlarmKosulUpdate")]
        public int Update(AlarmKosul alarmkosul)
        {
            return _alarmkosulDal.Update(alarmkosul);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IOTDelete, AlarmKosulDelete")]
        public int Delete(int Id)
        {
            return _alarmkosulDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, IOTDelete, AlarmKosulDelete")]
        public int DeleteSoft(int Id)
        {
            return _alarmkosulDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, IOTRead, AlarmKosulRead, AlarmKosulLtd")]
        public List<AlarmKosul> GetListPagination(PagingParams pagingParams)
        {
            return _alarmkosulDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _alarmkosulDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, IOTRead, AlarmKosulRead, AlarmKosulLtd")]
        public List<AlarmKosul> GetListAlarmKosulByAlarmID(int AlarmID)
        {
            return _alarmkosulDal.GetListAlarmKosulByAlarmID(AlarmID);
        }
    }

}