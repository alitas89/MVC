using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete.Iot;

namespace DataAccessLayer.Abstract.Iot
{
    public interface IAlarmKosulDal : IEntityRepository<AlarmKosul>
    {
        List<AlarmKosul> GetListAlarmKosulByAlarmID(int AlarmID);
    }
}