using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete.DatabaseModel;

namespace EntityLayer.Concrete.RequestModel
{
    public class ProductCategoryNamesDto
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string CategoryId { get; set; }
    }
}
