using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Varlik;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class VarlikGrupController : ApiController
    {
        IVarlikGrupService _varlikGrupService;

        public VarlikGrupController(IVarlikGrupService varlikGrupService)
        {
            _varlikGrupService = varlikGrupService;
        }

        // GET api/<controller>
        public IEnumerable<VarlikGrup> Get()
        {
            return _varlikGrupService.GetListDto();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _varlikGrupService.GetCountDto(filter) : _varlikGrupService.GetCountDto();
            var d = _varlikGrupService.GetListPaginationDto(new PagingParams()
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
        public VarlikGrup Get(int id)
        {
            return _varlikGrupService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]VarlikGrup varlikGrup)
        {
            return _varlikGrupService.Add(varlikGrup);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]VarlikGrup varlikGrup)
        {
            return _varlikGrupService.Update(varlikGrup);
        }

        public int Delete(int id)
        {
            return _varlikGrupService.DeleteSoft(id);
        }

        [Route("api/varlikgrup/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _varlikGrupService.Delete(id);
        }
    }
}