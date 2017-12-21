using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class Kisim : IEntity
    {
        public int KisimID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public decimal Butce { get; set; }
        public decimal HedeflenenButce { get; set; }
        public int VardiyaSinifID { get; set; }
        public int SarfYeriID { get; set; }
        public string Aciklama { get; set; }
    }
}
