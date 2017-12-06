using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using Core.Aspects.Postsharp.LogAspects;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using DapperExtensions;
using DapperExtensions.Mapper;
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


        //[LogAspect(typeof(DatabaseLogger))]
        //[LogAspect(typeof(FileLogger))]
        public List<Test> GetList()
        {
            return _testDal.GetList();
        }

        public Test GetById(int Id)
        {
            return _testDal.Get(Id);
        }

        public int Add(Test test)
        {
            return _testDal.Add(test);
        }

        public int Update(Test test)
        {
            return _testDal.Update(test);
        }

        public int Delete(int Id)
        {
            return _testDal.Delete(Id);
        }

        public int DeleteSoft(int Id)
        {
            return _testDal.DeleteSoft(Id);
        }
    }
}
