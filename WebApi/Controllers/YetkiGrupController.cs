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
    public class YetkiGrupController : ApiController
    {
        IYetkiGrupService _yetkiGrupService;

        public YetkiGrupController(IYetkiGrupService yetkiGrupService)
        {
            _yetkiGrupService = yetkiGrupService;
        }

        // GET api/<controller>
        public IEnumerable<YetkiGrup> Get()
        {
            return _yetkiGrupService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _yetkiGrupService.GetCount(filter) : _yetkiGrupService.GetCount();
            var d = _yetkiGrupService.GetListPagination(new PagingParams()
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
        public YetkiGrup Get(int id)
        {
            return _yetkiGrupService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]YetkiGrup yetkiGrup)
        {
            return _yetkiGrupService.Add(yetkiGrup);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]YetkiGrup yetkiGrup)
        {
            return _yetkiGrupService.Update(yetkiGrup);
        }

        public int Delete(int id)
        {
            return _yetkiGrupService.DeleteSoft(id);
        }

        [Route("api/yetkigrup/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _yetkiGrupService.Delete(id);
        }
    }
}