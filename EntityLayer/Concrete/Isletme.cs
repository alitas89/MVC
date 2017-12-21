using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class Isletme : IEntity
    {
        public int IsletmeID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string HaritaResmiYolu { get; set; }
        public string Aciklama { get; set; }
    }
}
