using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    //Sadece servis edilecek operasyonlar yazılır.
    //Repository ile aynı görünebilir ancak bu işlemler burada ayrılarak yapılmalıdır.
    //Çünkü servis operasyonları zamanla erozyona uğrar (ihtiyaca göre ciddi değişikliler oluşur)
    public interface ITestService
    {
        List<Test> GetList(int top = 0, string whereQuery = "", object parameters = null);

        Test GetById(int id);

        List<Test> GetByTestName(string testName);

        int Add(Test test);

    }
}