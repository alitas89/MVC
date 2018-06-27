using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace WebApi.Controllers
{
    //**WEB API CONTROLLER - PeriyodikBakimBildirimOkundu

    public class PeriyodikBakimBildirimOkunduController : ApiController
    {
        IPeriyodikBakimBildirimOkunduService _periyodikBakimBildirimOkunduService;

        public PeriyodikBakimBildirimOkunduController(IPeriyodikBakimBildirimOkunduService periyodikBakimBildirimOkunduService)
        {
            _periyodikBakimBildirimOkunduService = periyodikBakimBildirimOkunduService;
        }

        // GET api/<controller>
        public IEnumerable<PeriyodikBakimBildirimOkundu> Get()
        {
            return _periyodikBakimBildirimOkunduService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _periyodikBakimBildirimOkunduService.GetCount(filter) : _periyodikBakimBildirimOkunduService.GetCount();
            List<PeriyodikBakimBildirimOkundu> d = _periyodikBakimBildirimOkunduService.GetListPagination(new PagingParams()
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
        public PeriyodikBakimBildirimOkundu Get(int id)
        {
            return _periyodikBakimBildirimOkunduService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]PeriyodikBakimBildirimOkundu periyodikBakimBildirimOkundu)
        {
            return _periyodikBakimBildirimOkunduService.Add(periyodikBakimBildirimOkundu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]PeriyodikBakimBildirimOkundu periyodikBakimBildirimOkundu)
        {
            return _periyodikBakimBildirimOkunduService.Update(periyodikBakimBildirimOkundu);
        }

        public int Delete(int id)
        {
            return _periyodikBakimBildirimOkunduService.DeleteSoft(id);
        }

        [Route("api/periyodikbakimbildirimokundu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _periyodikBakimBildirimOkunduService.Delete(id);
        }
    }
}
