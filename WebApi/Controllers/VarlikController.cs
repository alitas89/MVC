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
using System.Linq.Dynamic;

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
        [Route("api/varlik/getlistbykisim/{KisimID}")]
        [HttpGet]
        public IEnumerable<Varlik> GetListByKisim(int KisimID)
        {
            return _varlikService.GetListByKisimID(KisimID);
        }

        [Route("api/varlik/getlistbykaynak{KaynakID}")]
        [HttpGet]
        public IEnumerable<Varlik> GetListByKaynak(int KaynakID)
        {
            return _varlikService.GetListByKaynakID(KaynakID);
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _varlikService.GetCountDto(filter) : _varlikService.GetCountDto();
            var d = _varlikService.GetListPaginationDto(new PagingParams()
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

        [Route("api/varlik/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _varlikService.Delete(id);
        }
    }
}