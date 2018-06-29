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
    //**WEB API CONTROLLER - PeriyodikBakimBildirimPushed

    public class PeriyodikBakimBildirimPushedController : ApiController
    {
        IPeriyodikBakimBildirimPushedService _periyodikBakimBildirimPushedService;

        public PeriyodikBakimBildirimPushedController(IPeriyodikBakimBildirimPushedService periyodikBakimBildirimPushedService)
        {
            _periyodikBakimBildirimPushedService = periyodikBakimBildirimPushedService;
        }

        // GET api/<controller>
        public IEnumerable<PeriyodikBakimBildirimPushed> Get()
        {
            return _periyodikBakimBildirimPushedService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _periyodikBakimBildirimPushedService.GetCount(filter) : _periyodikBakimBildirimPushedService.GetCount();
            List<PeriyodikBakimBildirimPushed> d = _periyodikBakimBildirimPushedService.GetListPagination(new PagingParams()
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
        public PeriyodikBakimBildirimPushed Get(int id)
        {
            return _periyodikBakimBildirimPushedService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]PeriyodikBakimBildirimPushed periyodikBakimBildirimPushed)
        {
            return _periyodikBakimBildirimPushedService.Add(periyodikBakimBildirimPushed);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]PeriyodikBakimBildirimPushed periyodikBakimBildirimPushed)
        {
            return _periyodikBakimBildirimPushedService.Update(periyodikBakimBildirimPushed);
        }

        public int Delete(int id)
        {
            return _periyodikBakimBildirimPushedService.DeleteSoft(id);
        }

        [Route("api/periyodikbakimbildirimpushed/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _periyodikBakimBildirimPushedService.Delete(id);
        }
    }
}
