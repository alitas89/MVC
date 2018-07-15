using BusinessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;
using EntityLayer.ComplexTypes.DtoModel.Iot;

namespace WebApi.Controllers
{
    public class SayacController : ApiController
    {
        ISayacService _sayacService;

        public SayacController(ISayacService sayacService)
        {
            _sayacService = sayacService;
        }

        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _sayacService.GetCountDto(filter) : _sayacService.GetCountDto();
            var d = _sayacService.GetListPaginationDto(new PagingParams()
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

        [Route("api/sayac/getlistbymodemserino")]
        public HttpResponseMessage GetListByModemSeriNo(int offset, int limit, string filter = "", string order = "", string columns = "", string modemserino = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _sayacService.GetCountDtoByModemSeriNo(modemserino, filter) : _sayacService.GetCountDtoByModemSeriNo(modemserino);
            var d = _sayacService.GetListPaginationDtoByModemSeriNo(new PagingParams()
            {
                filter = filter,
                limit = limit,
                offset = offset,
                order = order,
                columns = columns
            }, modemserino);
            var response = columns.Length > 0 ?
                Request.CreateResponse(HttpStatusCode.OK, d.Select("new(" + columns + ")").Cast<dynamic>().AsEnumerable().ToList())
                : Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
        }


        public int Post([FromBody]SayacKomut komut)
        {
            return _sayacService.AddSayacKomut(komut);
        }

    }
}