using Core.DataAccessLayer;
using EntityLayer.Concrete.DatabaseModel;

namespace DataAccessLayer.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {

    }
}