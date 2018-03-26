using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete.Genel
{
    public class YetkiGrup : IEntity
    {
        public int YetkiGrupID { get; set; }
        public string Kod { get; set; }
        public string Ad { get; set; }
        public bool Silindi { get; set; }
    }
}
