using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // GET api/<controller>
        public IEnumerable<Company> Get()
        {
            return _companyService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int? offset, int? limit, string filterCol="", string filterVal="")
        {
            int limitVal = limit ?? 100;
            int offSetVal = offset ?? 0;
            int total = _companyService.GetCount();

            var d = _companyService.GetListPagination(offSetVal, limitVal, filterCol, filterVal);
            var response = Request.CreateResponse(HttpStatusCode.OK, d);

            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");

            return response;
        }

        // GET api/<controller>/5
        public Company Get(int id)
        {
            return _companyService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Company company)
        {
            return _companyService.Add(company);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Company company)
        {
            return _companyService.Update(company);
        }

        public int Delete(int id)
        {
            return _companyService.DeleteSoft(id);
        }

        [Route("api/company/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _companyService.Delete(id);
        }
    }
}