using Core.DataAccessLayer;
using EntityLayer.Concrete.DatabaseModel;

namespace DataAccessLayer.Abstract
{
    public interface ICompanyDal : IEntityRepository<Company>
    {

    }
}