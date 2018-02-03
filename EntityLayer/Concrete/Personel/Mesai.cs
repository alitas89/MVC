using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Personel
{
    public class Mesai : IEntity
    {
        public int MesaiID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int UcretCarpani { get; set; }
        public int MesaiTuruID { get; set; }
        public bool Silindi { get; set; }
    }
}
