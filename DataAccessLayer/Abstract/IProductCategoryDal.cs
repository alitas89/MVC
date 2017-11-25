using Core.DataAccessLayer;
using EntityLayer.Concrete.DatabaseModel;

namespace DataAccessLayer.Abstract
{
    public interface IProductCategoryDal: IMultiEntityRepository<Product,Category>
    {
        
    }
}