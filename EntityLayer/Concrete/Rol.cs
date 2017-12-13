using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class Rol : IEntity
    {
        public int RolId { get; set; }
        public string Ad { get; set; }
    }
}
