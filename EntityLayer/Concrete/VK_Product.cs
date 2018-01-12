using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.EntityLayer;

namespace EntityLayer.Concrete
{
    public class VK_Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }
        public bool IsDeleted { get; set; }
        public Category Category { get; set; }
        public Company Company { get; set; }
    }
}
