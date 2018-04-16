using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;
namespace WebApi.Controllers
{
    public class YakitController : ApiController
    {
        IYakitService _yakitService;

        public YakitController(IYakitService yakitService)
        {
            _yakitService = yakitService;
        }

        // GET api/<controller>
        public IEnumerable<Yakit> Get()
        {
            return _yakitService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _yakitService.GetCount(filter) : _yakitService.GetCount();
            var d = _yakitService.GetListPagination(new PagingParams()
            {
                filter = filter,
                limit = limit,
                offset = offset,
                order = order,
                columns = columns
            });
            var response = columns.Length > 0 ?
            Request.CreateResponse(HttpStatusCode.OK, d.Select("new(" + columns + ")").Cast<dynamic>().AsEnumerable().ToList())
           : Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
        }
        // GET api/<controller>/5
        public Yakit Get(int id)
        {
            return _yakitService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Yakit yakit)
        {
            return _yakitService.Add(yakit);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Yakit yakit)
        {
            return _yakitService.Update(yakit);
        }

        public int Delete(int id)
        {
            return _yakitService.DeleteSoft(id);
        }

        [Route("api/yakit/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _yakitService.Delete(id);
        }
    }
}
