using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;

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

        [System.Web.Mvc.Route("api/company/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _companyService.Delete(id);
        }
    }
}