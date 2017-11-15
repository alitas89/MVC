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

namespace BusinessLayer.Concrete
{
    public class TestManager : ITestService
    {
        ITestDal _testDal;

        public TestManager(ITestDal testDal)
        {
            _testDal = testDal;
        }

        public List<Test> GetList(int top=0, string whereQuery="", object parameters=null)
        {
            string topSql = top == 0 ? "" : "TOP " + top;
            return _testDal.GetList($"select {topSql} * from Test"+ whereQuery, parameters);
        }

        //{p => (p.Id == value(BusinessLayer.Concrete.TestManager+<>c__DisplayClass3_0).id)}
        public Test GetById(int id)
        {
            //var predicate = Predicates.Field<Test>(f => f.Id, Operator.Eq, id);
            //return _testDal.Get(predicate);
            return _testDal.Get("select * from Test where Id=@id", new { id = id });
        }

        public List<Test> GetByTestName(string testName)
        {
            throw new NotImplementedException();
        }

        //public List<Test> GetByTestName(string productName)
        //{
        //    return _testDal.GetList(p => p.TestName.Contains(productName));
        //}

        public int Add(Test test)
        {
            //ValidatorTool.Validate(ProductValidator, product);
            //_testDal.Add(product);
            return _testDal.Add("insert Test(Id, Ip) values (@Id, @Ip)", test);
        }
    }

}
