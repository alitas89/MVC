using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    //Buraya standart istekler dışında bir yapı olursa yazılır 
    public interface ITestDal:IEntityRepository<Test>
    {
        //Burada kendisine özgü yapılar yazılabilir
    }
}