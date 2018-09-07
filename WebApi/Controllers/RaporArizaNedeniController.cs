using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;
using System.Data;

namespace WebApi.Controllers
{
    public class RaporArizaNedeniController : ApiController
    {
        IRaporArizaNedeniService _raporArizaNedeniService;

        public RaporArizaNedeniController(IRaporArizaNedeniService raporArizaNedeniService)
        {
            _raporArizaNedeniService = raporArizaNedeniService;
        }

        // GET api/<controller>
        public IEnumerable<ArizaNedeni> Get()
        {
            return new List<ArizaNedeni>();

        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "",
            string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _raporArizaNedeniService.GetCountDto(filter) : _raporArizaNedeniService.GetCountDto();
            var d = _raporArizaNedeniService.GetListPaginationDto(new PagingParams()
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
    }
}
