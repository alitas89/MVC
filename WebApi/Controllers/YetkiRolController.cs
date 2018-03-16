using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace WebApi.Controllers
{
    public class YetkiRolController : ApiController
    {
        IYetkiRolService _yetkiRolService;

        public YetkiRolController(IYetkiRolService yetkiRolService)
        {
            _yetkiRolService = yetkiRolService;
        }

        // GET api/<controller>
        public IEnumerable<YetkiRol> Get()
        {
            return _yetkiRolService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns="")
        {
            int total = 0;
            total = filter.Length != 0 ? _yetkiRolService.GetCount(filter) : _yetkiRolService.GetCount();
            var d = _yetkiRolService.GetListPagination(new PagingParams()
            {
                filter = filter,
                limit = limit,
                offset = offset,
                order = order,
                columns= columns
            });
            var response = Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
        }
        // GET api/<controller>/5
        public YetkiRol Get(int id)
        {
            return _yetkiRolService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]YetkiRol yetkiRol)
        {
            return _yetkiRolService.Add(yetkiRol);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]YetkiRol yetkiRol)
        {
            return _yetkiRolService.Update(yetkiRol);
        }

        public int Delete(int id)
        {
            return _yetkiRolService.DeleteSoft(id);
        }

        [Route("api/yetkirol/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _yetkiRolService.Delete(id);
        }
    }
}