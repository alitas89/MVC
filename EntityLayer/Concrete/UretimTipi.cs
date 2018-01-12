using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class UretimTipi : IEntity
    {
        public int UretimTipiID { get; set; }
        public string UretimTipiAd { get; set; }
    }

}
