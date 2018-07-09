using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Iot
{
    public class AlarmKosul : IEntity
    {
        public int AlarmKosulID { get; set; }
        public int AlarmID { get; set; }
        public int OznitelikID { get; set; }
        public int KosulID { get; set; }
        public decimal Deger { get; set; }
        public decimal Tolerans { get; set; }
        public DateTime Tarih { get; set; }
        public bool Silindi { get; set; }
    }
}