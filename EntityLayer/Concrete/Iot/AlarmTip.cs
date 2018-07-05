using Core.EntityLayer;

namespace EntityLayer.Concrete.Iot
{
    public class AlarmTip : IEntity
    {
        public int AlarmTipID { get; set; }
        public string Ad { get; set; }
        public bool Silindi { get; set; }
    }
}