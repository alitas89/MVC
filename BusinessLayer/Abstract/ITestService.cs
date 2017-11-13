using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    //Sadece servis edilecek operasyonlar yazılır.
    //Repository ile aynı görünebilir ancak bu işlemler burada ayrılarak yapılmalıdır.
    //Çünkü servis operasyonları zamanla erozyona uğrar (ihtiyaca göre ciddi değişikliler oluşur)
    public interface ITestService
    {
        List<Test> GetList();

        Test GetById(int id);

        List<Test> GetByTestName(string testName);

        void Add(Test test);

    }
}