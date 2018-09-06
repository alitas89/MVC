using BusinessLayer.Abstract.Bakim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EntityLayer.ComplexTypes.ParameterModel;
using System.Linq.Dynamic;
using System.Data;
using EntityLayer.Concrete.Bakim;

namespace WebApi.Controllers
{
    public class RaporIsEmriByVarlikController : ApiController
    {
        IRaporIsEmriByVarlikService _raporIsEmriByVarlikService;

        public RaporIsEmriByVarlikController(IRaporIsEmriByVarlikService raporIsEmriByVarlikService)
        {
            _raporIsEmriByVarlikService = raporIsEmriByVarlikService;
        }

        // GET api/<controller>
        public IEnumerable<IsEmri> Get()
        {
            return new List<IsEmri>();

        }

        // GET api/<controller>
        public HttpResponseMessage Get(int VarlikID, int offset, int limit, string filter = "",
            string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _raporIsEmriByVarlikService.GetCountDtoByVarlikID(VarlikID, filter) : _raporIsEmriByVarlikService.GetCountDtoByVarlikID(VarlikID);
            var d = _raporIsEmriByVarlikService.GetListPaginationDtoByVarlikID(VarlikID, new PagingParams()
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
