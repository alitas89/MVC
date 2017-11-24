using System.Collections.Generic;
using EntityLayer.Concrete.DatabaseModel;
using EntityLayer.Concrete.RequestModel;

namespace BusinessLayer.Abstract
{
    //Sadece servis edilecek operasyonlar yazılır.
    //Repository ile aynı görünebilir ancak bu işlemler burada ayrılarak yapılmalıdır.
    //Çünkü servis operasyonları zamanla erozyona uğrar (ihtiyaca göre ciddi değişikliler oluşur)
    public interface ITestService
    {
        //Burada özel olarak TestRequestModel dönecez
        List<TestRequestModel> GetListRequest(string query, object parameters = null);

        List<Test> GetList(int top = 0, string whereQuery = "", object parameters = null);

        Test GetById(int id);

        int Add(Test test);

        int Update(Test test);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}