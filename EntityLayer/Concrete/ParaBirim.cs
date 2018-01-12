using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class ParaBirim : IEntity
    {
        public int ParaBirimID { get; set; }
        public string ParaBirimAd { get; set; }
    }
}
