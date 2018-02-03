using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Personel
{
    public class KaynakPozisyonu : IEntity
    {
        public int KaynakPozisyonuID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int UstDuzeyPozisyonID { get; set; }
        public string Aciklama { get; set; }
        public bool Teknisyendir { get; set; }
        public bool Silindi { get; set; }
    }
}
