using System;

namespace EntityLayer.ComplexTypes.DtoModel.Iot
{
    public class AlarmDto
    {
        public string AlarmID { get; set; }
        public string Ad { get; set; }
        public string AlarmTipAd { get; set; }
        public string IsTipiAd { get; set; }
        public DateTime Tarih { get; set; }
        public string VarlikAd { get; set; }
        public bool Silindi { get; set; }
    }
}