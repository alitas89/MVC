using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace WebApi.Controllers
{
    public class YasalTakipController : ApiController
    {
        IYasalTakipService _yasalTakipService;

        public YasalTakipController(IYasalTakipService yasalTakipService)
        {
            _yasalTakipService = yasalTakipService;
        }

        // GET api/<controller>
        public IEnumerable<YasalTakip> Get()
        {
            return _yasalTakipService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _yasalTakipService.GetCount(filter) : _yasalTakipService.GetCount();
            List<YasalTakip> d = _yasalTakipService.GetListPagination(new PagingParams()
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
        public YasalTakip Get(int id)
        {
            return _yasalTakipService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]YasalTakip yasalTakip)
        {
            return _yasalTakipService.Add(yasalTakip);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]YasalTakip yasalTakip)
        {
            return _yasalTakipService.Update(yasalTakip);
        }

        public int Delete(int id)
        {
            return _yasalTakipService.DeleteSoft(id);
        }

        [Route("api/yasaltakip/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _yasalTakipService.Delete(id);
        }


        [Route("api/yasaltakip/getyasaltakipbyvarlikid/{VarlikID}")]
        public YasalTakip GetYasalTakipByVarlikID(int VarlikID)
        {
            return _yasalTakipService.GetYasalTakipByVarlikID(VarlikID);
        }
    }
}
