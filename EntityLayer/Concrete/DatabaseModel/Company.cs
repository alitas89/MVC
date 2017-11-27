using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete.DatabaseModel
{
    public class Company : IEntity
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } 
        public bool IsDeleted { get; set; }
    }
}
