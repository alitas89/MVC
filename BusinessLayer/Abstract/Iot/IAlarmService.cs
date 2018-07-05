using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;

namespace BusinessLayer.Abstract.Iot
{
    public interface IAlarmService
    {
        List<Alarm> GetList();

        Alarm GetById(int id);

        int Add(Alarm alarm);

        int Update(Alarm alarm);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Alarm> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");
    }
}