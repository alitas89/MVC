using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.Concrete.DatabaseModel;
using UtilityLayer.Filters;

namespace WebApi.Controllers
{
    [CustomException]
    public class TestController : ApiController
    {
        ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        // GET api/<controller>
        public IEnumerable<Test> Get()
        {
            //return _testService.GetList(1, " where Ip=@Ip", new { Ip = "Bulunamadı!" });
            return _testService.GetList();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            Test test = _testService.GetById(id);
            return test.Ip;
        }

        // POST api/<controller>
        [CustomAction]
        public int Post([FromBody]Test test)
        {
           return _testService.Add(test);
         
        }

        // PUT api/<controller>/5
        [CustomAction]
        public int Put([FromBody]Test test)
        {
            int sayi = _testService.Update(test);
            return sayi;
        }

        // DELETE api/<controller>/5
        [CustomAction]
        public int Delete(int id)
        {
           int sayi = _testService.DeleteSoft(id);
           return sayi;
        }

        // DELETE api/<controller>/5
        [CustomAction]
        [Route("api/test/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            int sayi = _testService.Delete(id);
            return sayi;
        }
    }
}