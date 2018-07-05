using Core.EntityLayer;

namespace EntityLayer.Concrete.Iot
{
    public class AlarmKosulTip : IEntity
    {
        public int AlarmKosulTipID { get; set; }
        public string Ad { get; set; }
        public bool Silindi { get; set; }
    }
}