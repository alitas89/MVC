using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class Birim : IEntity
    {
        public int BirimID { get; set; }
        public string BirimAd { get; set; }
    }
}
