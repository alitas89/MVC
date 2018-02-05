using Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Satinalma
{
    public class TeklifIstemeSablon : IEntity
    {
        public int TeklifIstemeSablonID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int KisimID { get; set; }
        public int SarfYeriID { get; set; }
        public int BelgeTuruID { get; set; }
        public int AciliyetID { get; set; }
        public int MasrafTuruID { get; set; }
        public bool TedarikciOnayli { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}
