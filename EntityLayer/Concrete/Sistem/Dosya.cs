using Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Sistem
{
    public class Dosya : IEntity
    {
        public int DosyaID { get; set; }
        public int BagliID { get; set; }
        public string Ad { get; set; }
        public string Path { get; set; }
        public int DosyaModul { get; set; }
        public DateTime YuklenmeTarih { get; set; }
        public bool Silindi { get; set; }
    }
}
