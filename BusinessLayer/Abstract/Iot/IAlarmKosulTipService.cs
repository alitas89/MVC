using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;

namespace BusinessLayer.Abstract.Iot
{
    public interface IAlarmKosulTipService
    {
        List<AlarmKosulTip> GetList();

        AlarmKosulTip GetById(int id);

        int Add(AlarmKosulTip alarmkosultip);

        int Update(AlarmKosulTip alarmkosultip);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<AlarmKosulTip> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");
    }

}