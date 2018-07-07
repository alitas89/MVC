using System;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Iot
{
    public class Alarm : IEntity
    {
        public int AlarmID { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public int IsTipiID { get; set; }
        public int AlarmTipID { get; set; }
        public int OlusturanID { get; set; }
        public int Tolerans { get; set; }
        public int VarlikID { get; set; }
        public DateTime Tarih { get; set; }
        public bool Silindi { get; set; }
    }
}