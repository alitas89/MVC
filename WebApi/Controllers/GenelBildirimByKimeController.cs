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
    public class GenelBildirimByKimeController : ApiController
    {
        IGenelBildirimService _genelBildirimService;

        public GenelBildirimByKimeController(IGenelBildirimService genelBildirimService)
        {
            _genelBildirimService = genelBildirimService;
        }


        public HttpResponseMessage Get(int kullaniciID, int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;

            total = filter.Length != 0 ? _genelBildirimService.GetCountByKime(kullaniciID, filter) : _genelBildirimService.GetCountByKime(kullaniciID);
            List<GenelBildirim> d = _genelBildirimService.GetListPaginationByKime(new PagingParams()
            {
                filter = filter,
                limit = limit,
                offset = offset,
                order = order,
                columns = columns
            }, kullaniciID);
            var response = columns.Length > 0 ?
                Request.CreateResponse(HttpStatusCode.OK, d.Select("new(" + columns + ")").Cast<dynamic>().AsEnumerable().ToList())
                : Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
        }
    }
}
