using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class KullaniciRol : IEntity
    {
        public int KullaniciRolId { get; set; }
        public int KullaniciId { get; set; }
        public int RolId { get; set; }
        public bool Silindi { get; set; }
    }
}
