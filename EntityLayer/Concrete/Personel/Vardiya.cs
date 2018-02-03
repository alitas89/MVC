using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Personel
{
    public class Vardiya : IEntity
    {
        public int VardiyaID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string BaslangicSaati { get; set; }
        public string BaslangicSaati2 { get; set; }
        public string BitisSaati { get; set; }
        public string BitisSaati2 { get; set; }
        public int SarfYeriID { get; set; }
        public bool BakimSuresiHesabinaDahil { get; set; }
        public bool DurusSuresiHesabinaDahil { get; set; }
        public bool Silindi { get; set; }
    }
}
