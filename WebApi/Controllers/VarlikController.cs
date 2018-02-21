using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace WebApi.Controllers
{
    public class VarlikController : ApiController
    {
        IVarlikService _varlikService;

        public VarlikController(IVarlikService varlikService)
        {
            _varlikService = varlikService;
        }

        // GET api/<controller>
        public IEnumerable<Varlik> Get()
        {
            return _varlikService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _varlikService.GetCountDto(filterCol, filterVal) : _varlikService.GetCountDto();
            var d = _varlikService.GetListPaginationDto(new PagingParams()
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
        public Varlik Get(int id)
        {
            return _varlikService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Varlik varlik)
        {
            return _varlikService.Add(varlik);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Varlik varlik)
        {
            return _varlikService.Update(varlik);
        }

        public int Delete(int id)
        {
            return _varlikService.DeleteSoft(id);
        }

        [System.Web.Mvc.Route("api/varlik/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _varlikService.Delete(id);
        }
    }
}