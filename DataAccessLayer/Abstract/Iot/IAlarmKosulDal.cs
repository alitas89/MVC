using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Iot;
using EntityLayer.Concrete.Iot;

namespace DataAccessLayer.Abstract.Iot
{
    public interface IAlarmKosulDal : IEntityRepository<AlarmKosul>
    {
        List<AlarmKosulDto> GetListAlarmKosulByAlarmID(int AlarmID);
    }
}