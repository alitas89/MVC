using Core.DataAccessLayer;
using EntityLayer.Concrete.DatabaseModel;

namespace DataAccessLayer.Abstract
{
    public interface IProductWithCategoryDal2 : IEntityMultiRepository<Product, Category, Product>
    {
        
    }
}