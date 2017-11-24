using Core.DataAccessLayer;
using EntityLayer.Concrete.DatabaseModel;

namespace DataAccessLayer.Abstract
{
    public interface ICategoryDal : IEntityRepository<Category>
    {

    }
}