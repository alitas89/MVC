using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace WebApi.Controllers
{
    public class BakimArizaKoduController : ApiController
    {
        IBakimArizaKoduService _bakimArizaKoduService;

        public BakimArizaKoduController(IBakimArizaKoduService bakimArizaKoduService)
        {
            _bakimArizaKoduService = bakimArizaKoduService;
        }

        // GET api/<controller>
        public IEnumerable<BakimArizaKodu> Get()
        {
            return _bakimArizaKoduService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _bakimArizaKoduService.GetCountDto(filterCol, filterVal) : _bakimArizaKoduService.GetCountDto();
            var d = _bakimArizaKoduService.GetListPaginationDto(new PagingParams()
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
        public BakimArizaKodu Get(int id)
        {
            return _bakimArizaKoduService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BakimArizaKodu bakimArizaKodu)
        {
            return _bakimArizaKoduService.Add(bakimArizaKodu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BakimArizaKodu bakimArizaKodu)
        {
            return _bakimArizaKoduService.Update(bakimArizaKodu);
        }

        public int Delete(int id)
        {
            return _bakimArizaKoduService.DeleteSoft(id);
        }

        [Route("api/bakimarizakodu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bakimArizaKoduService.Delete(id);
        }


    }
}