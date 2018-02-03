using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Personel
{
    public class MesaiTuru : IEntity
    {
        public int MesaiTuruID { get; set; }
        public string MesaiTuruAd { get; set; }
    }
}
