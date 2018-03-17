using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace WebApi.Controllers
{
    public class IsEmriController : ApiController
    {
        IIsEmriService _isEmriService;

        public IsEmriController(IIsEmriService isEmriService)
        {
            _isEmriService = isEmriService;
        }

        // GET api/<controller>
        public IEnumerable<IsEmri> Get()
        {
            return _isEmriService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _isEmriService.GetCountDto(filter) : _isEmriService.GetCountDto();
            var d = _isEmriService.GetListPaginationDto(new PagingParams()
            {
                filter = filter,
                limit = limit,
                offset = offset,
                order = order,
                columns = columns
            });
            var response = Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
        }
        // GET api/<controller>/5
        public IsEmri Get(int id)
        {
            return _isEmriService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]IsEmri isEmri)
        {
            return _isEmriService.Add(isEmri);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsEmri isEmri)
        {
            return _isEmriService.Update(isEmri);
        }

        public int Delete(int id)
        {
            return _isEmriService.DeleteSoft(id);
        }

        [Route("api/isemri/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isEmriService.Delete(id);
        }
    }
}