using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;

namespace BusinessLayer.Abstract.Iot
{
    public interface IAlarmTipService
    {
        List<AlarmTip> GetList();

        AlarmTip GetById(int id);

        int Add(AlarmTip alarmtip);

        int Update(AlarmTip alarmtip);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<AlarmTip> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");
    }
}