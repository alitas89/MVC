using Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Satinalma
{
    public class TeslimSekli : IEntity
    {
        public int TeslimSekliID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public string Aciklama { get; set; }
        public bool VarsayilanDeger { get; set; }
        public bool Silindi { get; set; }
    }
}
