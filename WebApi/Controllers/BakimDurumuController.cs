using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace WebApi.Controllers
{
    public class BakimDurumuController : ApiController
    {
        IBakimDurumuService _bakimDurumuService;

        public BakimDurumuController(IBakimDurumuService bakimDurumuService)
        {
            _bakimDurumuService = bakimDurumuService;
        }

        // GET api/<controller>
        public IEnumerable<BakimDurumu> Get()
        {
            return _bakimDurumuService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _bakimDurumuService.GetCount(filter) : _bakimDurumuService.GetCount();
            List<BakimDurumu> d = _bakimDurumuService.GetListPagination(new PagingParams()
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
        public BakimDurumu Get(int id)
        {
            return _bakimDurumuService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BakimDurumu bakimDurumu)
        {
            return _bakimDurumuService.Add(bakimDurumu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BakimDurumu bakimDurumu)
        {
            return _bakimDurumuService.Update(bakimDurumu);
        }

        public int Delete(int id)
        {
            return _bakimDurumuService.DeleteSoft(id);
        }

        [Route("api/bakimdurumu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bakimDurumuService.Delete(id);
        }
    }
}