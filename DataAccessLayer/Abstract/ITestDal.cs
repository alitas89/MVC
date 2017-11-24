using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete.DatabaseModel;

namespace DataAccessLayer.Abstract
{
    public interface ITestDal:IEntityRepository<Test>
    {

    }
}