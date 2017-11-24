using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DapperExtensions;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Concrete.DatabaseModel;

namespace BusinessLayer.Concrete
{

    public class TestManager : ITestService
    {
        ITestDal _testDal;

        public TestManager(ITestDal testDal)
        {
            _testDal = testDal;
        }

        public List<Test> GetList(int top = 0, string whereQuery = "", object parameters = null)
        {
            string topSql = top == 0 ? "" : "TOP " + top;
            return _testDal.GetList($"select {topSql}* from Test" + whereQuery, parameters);
        }

        public Test GetById(int Id)
        {
            return _testDal.Get("select *  from Test where Id = @Id", new {Id = Id});
        }

        public int Add(Test test)
        {
            return _testDal.Add("insert Test(Ip,IsDeleted) values (@Ip,@IsDeleted)", test);
        }

        public int Update(Test test)
        {
            return _testDal.Update("update Test set Ip=@Ip,IsDeleted=@IsDeleted where Id=@Id", test);
        }

        public int Delete(int Id)
        {
            return _testDal.Delete("delete from Test where Id=@Id", new {Id = Id});
        }

        public int DeleteSoft(int Id)
        {
            return _testDal.Update("update Test set IsDeleted = 1 where Id=@Id", new {Id = Id});
        }
    }
}
