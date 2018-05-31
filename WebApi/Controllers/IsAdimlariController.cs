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
    public class IsAdimlariController : ApiController
    {
        IIsAdimlariService _isAdimlariService;

        public IsAdimlariController(IIsAdimlariService isAdimlariService)
        {
            _isAdimlariService = isAdimlariService;
        }

        // GET api/<controller>
        public IEnumerable<IsAdimlari> Get()
        {
            return _isAdimlariService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _isAdimlariService.GetCount(filter) : _isAdimlariService.GetCount();
            List<IsAdimlari> d = _isAdimlariService.GetListPagination(new PagingParams()
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
        public IsAdimlari Get(int id)
        {
            return _isAdimlariService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]IsAdimlari isAdimlari)
        {
            return _isAdimlariService.Add(isAdimlari);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsAdimlari isAdimlari)
        {
            return _isAdimlariService.Update(isAdimlari);
        }

        public int Delete(int id)
        {
            return _isAdimlariService.DeleteSoft(id);
        }

        [Route("api/ısadimlari/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isAdimlariService.Delete(id);
        }

        [Route("api/isadimlari/getlistisadimlaribybakimplaniid/{BakimPlaniID}")]
        public List<IsAdimlari> GetListIsAdimlariByBakimPlaniID(int BakimPlaniID)
        {
            return _isAdimlariService.GetListIsAdimlariByBakimPlaniID(BakimPlaniID);
        }
    }
}
