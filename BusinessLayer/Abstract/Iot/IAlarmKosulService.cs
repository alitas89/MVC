using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;

namespace BusinessLayer.Abstract.Iot
{
    public interface IAlarmKosulService
    {
        List<AlarmKosul> GetList();

        AlarmKosul GetById(int id);

        int Add(AlarmKosul alarmkosul);

        int Update(AlarmKosul alarmkosul);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<AlarmKosul> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");

        List<AlarmKosul> GetListAlarmKosulByAlarmID(int AlarmID);
    }
}