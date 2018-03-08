using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Varlik;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{

    public class KisimController : ApiController
    {
        IKisimService _kisimService;

        public KisimController(IKisimService kisimService)
        {
            _kisimService = kisimService;
        }

        // GET api/<controller>
        public IEnumerable<Kisim> Get()
        {
            return _kisimService.GetListDto();
        }


        // GET api/<controller>
        [Route("api/kisim/getlistbysarfyeri/{SarfYeriID}")]
        [HttpGet]
        public IEnumerable<Kisim> GetListBySarfYeri(int SarfYeriID)
        {
            return _kisimService.GetList(SarfYeriID);
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _kisimService.GetCountDto(filterCol, filterVal) : _kisimService.GetCountDto();
            var d = _kisimService.GetListPaginationDto(new PagingParams()
            {
                filterCol = filterCol,
                filterVal = filterVal,
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
        public Kisim Get(int id)
        {
            return _kisimService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Kisim kisim)
        {
            return _kisimService.Add(kisim);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Kisim kisim)
        {
            return _kisimService.Update(kisim);
        }

        public int Delete(int id)
        {
            return _kisimService.DeleteSoft(id);
        }

        [Route("api/kisim/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _kisimService.Delete(id);
        }
    }
}