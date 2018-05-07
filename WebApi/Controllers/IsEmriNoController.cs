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
    public class IsEmriNoController : ApiController
    {
        IIsEmriNoService _isEmriNoService;

        public IsEmriNoController(IIsEmriNoService isEmriNoService)
        {
            _isEmriNoService = isEmriNoService;
        }

        // GET api/<controller>
        public IEnumerable<IsEmriNo> Get()
        {
            return _isEmriNoService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _isEmriNoService.GetCount(filter) : _isEmriNoService.GetCount();
            List<IsEmriNo> d = _isEmriNoService.GetListPagination(new PagingParams()
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
        public IsEmriNo Get(int id)
        {
            return _isEmriNoService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]IsEmriNo isEmriNo)
        {
            return _isEmriNoService.Add(isEmriNo);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsEmriNo isEmriNo)
        {
            return _isEmriNoService.Update(isEmriNo);
        }

        public int Delete(int id)
        {
            return _isEmriNoService.DeleteSoft(id);
        }

        [Route("api/ısemrino/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isEmriNoService.Delete(id);
        }
    }
}