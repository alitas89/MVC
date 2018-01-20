using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class Company : IEntity
    {
        public int CompanyId { get; set; }
        public string Ad { get; set; }
        public bool Silindi { get; set; }
    }
}
