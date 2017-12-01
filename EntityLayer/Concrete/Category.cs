using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class Category : IEntity
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public bool IsDeleted { get; set; }
    }
}
