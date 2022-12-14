using Core.EntityLayer;
using System;

namespace EntityLayer.Concrete.Iot
{
    public class Sayac : IEntity
    {
        public string SayacNo { get; set; }
        public double Tuketim { get; set; }
        public double Pil { get; set; }
        public DateTime Tarih { get; set; }
        public string ModemSeriNo { get; set; }
        public string SayacCapi { get; set; }
        public string EtiketNo { get; set; }
        public string Ceza { get; set; }
        public string Endeks { get; set; }
        public bool Silindi { get; set; }
        public int BagliVarlikKod { get; set; }
    }
}
