using System;
using Core.DataAccessLayer;
using EntityLayer.Concrete.DatabaseModel;

namespace DataAccessLayer.Abstract
{
    public interface IProductCategoryDal: IMulti2EntityRepository<Product,Category>
    {
        
    }
}