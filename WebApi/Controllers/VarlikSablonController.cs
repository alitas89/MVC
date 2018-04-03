using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Linq.Dynamic;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class VarlikSablonController : ApiController
    {
        IVarlikSablonService _varlikSablonService;

        public VarlikSablonController(IVarlikSablonService varlikSablonService)
        {
            _varlikSablonService = varlikSablonService;
        }

        // GET api/<controller>
        public IEnumerable<VarlikSablon> Get()
        {
            return _varlikSablonService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _varlikSablonService.GetCountDto(filter) : _varlikSablonService.GetCountDto();
            var d = _varlikSablonService.GetListPaginationDto(new PagingParams()
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
        public VarlikSablon Get(int id)
        {
            return _varlikSablonService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]VarlikSablon varlikSablon)
        {
            return _varlikSablonService.Add(varlikSablon);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]VarlikSablon varlikSablon)
        {
            return _varlikSablonService.Update(varlikSablon);
        }

        public int Delete(int id)
        {
            return _varlikSablonService.DeleteSoft(id);
        }

        [Route("api/varliksablon/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _varlikSablonService.Delete(id);
        }
    }
}