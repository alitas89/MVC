using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;

namespace WebApi.Controllers
{
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
            return _testService.GetList(1, " where Ip=@Ip", new { Ip = "Bulunamadı!" });
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            Test test = _testService.GetById(id);
            return test.Ip;
        }

        // POST api/<controller>
        public int Post([FromBody]Test test)
        {
            int sayi = _testService.Add(test);
            return sayi;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}