using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class Hizmet : IEntity
    {
        public int HizmetID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public decimal BirimFiyat { get; set; }
        public int ParaBirimID { get; set; }
        public string Aciklama { get; set; }
        public bool Silindi { get; set; }
    }
}
