using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Varlik;

namespace WebApi.Controllers
{
    public class SarfYeriController : ApiController
    {
        ISarfYeriService _sarfYeriService;

        public SarfYeriController(ISarfYeriService sarfYeriService)
        {
            _sarfYeriService = sarfYeriService;
        }

        // GET api/<controller>
        public IEnumerable<SarfYeriDto> Get()
        {
            return _sarfYeriService.GetListDto();
        }

        // GET api/<controller>
        [Route("api/sarfyeri/getlistbyisletme/{IsletmeID}")]
        [HttpGet]
        public IEnumerable<SarfYeri> GetListByIsletme(int IsletmeID)
        {
            return _sarfYeriService.GetList(IsletmeID);
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _sarfYeriService.GetCountDto(filterCol, filterVal) : _sarfYeriService.GetCountDto();
            var d = _sarfYeriService.GetListPaginationDto(new PagingParams()
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
        public SarfYeri Get(int id)
        {
            return _sarfYeriService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]SarfYeri sarfYeri)
        {
            return _sarfYeriService.Add(sarfYeri);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]SarfYeri sarfYeri)
        {
            return _sarfYeriService.Update(sarfYeri);
        }

        public int Delete(int id)
        {
            return _sarfYeriService.DeleteSoft(id);
        }

        [Route("api/sarfyeri/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _sarfYeriService.Delete(id);
        }
    }
}