using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer;
using EntityLayer.Concrete.DatabaseModel;

namespace DataAccessLayer.Abstract
{
    interface IProductWithCategoryDal : IEntityMultiRepository<Product, Category, Product>
    {
    }
}
