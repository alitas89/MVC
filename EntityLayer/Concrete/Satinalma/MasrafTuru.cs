using Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Satinalma
{
    public class MasrafTuru : IEntity
    {
        public int MasrafTuruID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public int KaynakPozisyonuID { get; set; }
        public bool SatinalmaVarsayilani { get; set; }
        public bool İsEmriVarsayilani { get; set; }
        public bool MalzemeVarsayilani { get; set; }
        public bool Silindi { get; set; }
    }
}
