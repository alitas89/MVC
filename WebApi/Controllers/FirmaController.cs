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
    //**WEB API CONTROLLER - Firma

    public class FirmaController : ApiController
    {
        IFirmaService _firmaService;

        public FirmaController(IFirmaService firmaService)
        {
            _firmaService = firmaService;
        }

        // GET api/<controller>
        public IEnumerable<Firma> Get()
        {
            return _firmaService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _firmaService.GetCount(filter) : _firmaService.GetCount();
            List<Firma> d = _firmaService.GetListPagination(new PagingParams()
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
        public Firma Get(int id)
        {
            return _firmaService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Firma firma)
        {
            return _firmaService.Add(firma);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Firma firma)
        {
            return _firmaService.Update(firma);
        }

        public int Delete(int id)
        {
            return _firmaService.DeleteSoft(id);
        }

        [Route("api/firma/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _firmaService.Delete(id);
        }
    }
}
