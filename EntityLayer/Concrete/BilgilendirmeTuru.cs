using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class BilgilendirmeTuru : IEntity
    {
        public int BilgilendirmeTuruID { get; set; }
        public string BilgilendirmeTuruAd { get; set; }
        public bool Silindi { get; set; }
    }
}
