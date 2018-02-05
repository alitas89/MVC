using Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Satinalma
{
    public class OdemeSekli : IEntity
    {
        public int OdemeSekliID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public int GunSayisi { get; set; }
        public string Aciklama { get; set; }
        public bool VarsayilanDeger { get; set; }
        public bool Silindi { get; set; }
    }
}
