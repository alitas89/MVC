using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Linq.Dynamic;
using System.Web.Script.Serialization;
using System.Xml;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using UtilityLayer.Tools;
using DynamicExpression = System.Linq.Dynamic.DynamicExpression;

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
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _sarfYeriService.GetCountDto(filterCol, filterVal) : _sarfYeriService.GetCountDto();
            List<SarfYeriDto> d = _sarfYeriService.GetListPaginationDto(new PagingParams()
            {
                filterCol = filterCol,
                filterVal = filterVal,
                limit = limit,
                offset = offset,
                order = order,
                columns = columns
            });

            //"new(Ad as Ad,Kod as Kod)"
            //Dynamic Linq ile Export alınacak yapı hazırlanır
            var resultDynamicLinq = d.Select("new(" + columns + ")").Cast<dynamic>().AsEnumerable().ToList();

            var response = columns.Length > 0 ? Request.CreateResponse(HttpStatusCode.OK, resultDynamicLinq)
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