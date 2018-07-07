using System.Collections.Generic;
using EntityLayer.Concrete.Iot;

namespace EntityLayer.ComplexTypes.DtoModel.Iot
{
    public class AlarmTemp
    {
        public Alarm alarm { get; set; }
        public List<AlarmKosul> listAlarmKosul { get; set; }
    }
}