﻿using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _sarfYeriService.GetCountDto(filter) : _sarfYeriService.GetCountDto();
            List<SarfYeriDto> d = _sarfYeriService.GetListPaginationDto(new PagingParams()
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