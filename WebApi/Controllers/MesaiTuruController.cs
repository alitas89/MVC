using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class MesaiTuruController : ApiController
    {
        IMesaiTuruService _mesaiTuruService;

        public MesaiTuruController(IMesaiTuruService mesaiTuruService)
        {
            _mesaiTuruService = mesaiTuruService;
        }

        // GET api/<controller>
        public IEnumerable<MesaiTuru> Get()
        {
            return _mesaiTuruService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _mesaiTuruService.GetCount(filter) : _mesaiTuruService.GetCount();
            var d = _mesaiTuruService.GetListPagination(new PagingParams()
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
        public MesaiTuru Get(int id)
        {
            return _mesaiTuruService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]MesaiTuru mesaiTuru)
        {
            return _mesaiTuruService.Add(mesaiTuru);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]MesaiTuru mesaiTuru)
        {
            return _mesaiTuruService.Update(mesaiTuru);
        }

        public int Delete(int id)
        {
            return _mesaiTuruService.DeleteSoft(id);
        }

        [Route("api/mesaituru/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _mesaiTuruService.Delete(id);
        }
    }
}