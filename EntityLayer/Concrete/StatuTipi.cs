using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class StatuTipi : IEntity
    {
        public int StatuTipiID { get; set; }
        public string StatuTipiAd { get; set; }
    }
}
