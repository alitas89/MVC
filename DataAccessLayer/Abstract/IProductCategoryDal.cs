using System;
using Core.DataAccessLayer;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IProductCategoryDal: IMulti2EntityRepository<Product,Category>
    {
        
    }
}