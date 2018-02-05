using Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Satinalma
{
    public class TeslimYeri : IEntity
    {
        public int TeslimYeriID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int KisimID { get; set; }
        public int SarfYeriID { get; set; }
        public int IsletmeID { get; set; }
        public string Semt { get; set; }
        public string Sehir { get; set; }
        public string Ulke { get; set; }
        public string Telefon1 { get; set; }
        public string Telefon2 { get; set; }
        public string Adres { get; set; }
        public bool Silindi { get; set; }
    }
}
