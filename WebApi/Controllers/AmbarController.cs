using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace WebApi.Controllers
{
    public class AmbarController : ApiController
    {
        IAmbarService _ambarService;

        public AmbarController(IAmbarService ambarService)
        {
            _ambarService = ambarService;
        }

        // GET api/<controller>
        public IEnumerable<Ambar> Get()
        {
            return _ambarService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _ambarService.GetCount(filterCol, filterVal) : _ambarService.GetCount();
            var d = _ambarService.GetListPagination(new PagingParams()
            {
                filterCol = filterCol,
                filterVal = filterVal,
                limit = limit,
                offset = offset,
                order = order
            });
            var response = Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
        }
        // GET api/<controller>/5
        public Ambar Get(int id)
        {
            return _ambarService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Ambar ambar)
        {
            return _ambarService.Add(ambar);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Ambar ambar)
        {
            return _ambarService.Update(ambar);
        }

        public int Delete(int id)
        {
            return _ambarService.DeleteSoft(id);
        }

        [System.Web.Mvc.Route("api/ambar/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _ambarService.Delete(id);
        }
    }
}