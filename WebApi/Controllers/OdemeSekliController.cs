using BusinessLayer.Abstract.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Satinalma;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class OdemeSekliController : ApiController
    {
        IOdemeSekliService _odemeSekliService;

        public OdemeSekliController(IOdemeSekliService odemeSekliService)
        {
            _odemeSekliService = odemeSekliService;
        }

        // GET api/<controller>
        public IEnumerable<OdemeSekli> Get()
        {
            return _odemeSekliService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _odemeSekliService.GetCount(filterCol, filterVal) : _odemeSekliService.GetCount();
            var d = _odemeSekliService.GetListPagination(new PagingParams()
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
        public OdemeSekli Get(int id)
        {
            return _odemeSekliService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]OdemeSekli odemeSekli)
        {
            return _odemeSekliService.Add(odemeSekli);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]OdemeSekli odemeSekli)
        {
            return _odemeSekliService.Update(odemeSekli);
        }

        public int Delete(int id)
        {
            return _odemeSekliService.DeleteSoft(id);
        }

        [Route("api/odemesekli/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _odemeSekliService.Delete(id);
        }
    }
}