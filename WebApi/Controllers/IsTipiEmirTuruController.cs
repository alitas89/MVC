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
using EntityLayer.ComplexTypes.DtoModel.Bakim;

namespace WebApi.Controllers
{
    public class IsTipiEmirTuruController : ApiController
    {
        IIsTipiEmirTuruService _isTipiEmirTuruService;

        public IsTipiEmirTuruController(IIsTipiEmirTuruService isTipiEmirTuruService)
        {
            _isTipiEmirTuruService = isTipiEmirTuruService;
        }

        // GET api/<controller>
        public IEnumerable<IsTipiEmirTuru> Get()
        {
            return _isTipiEmirTuruService.GetList();
        }

        // GET api/<controller>
        [Route("api/istipiemirturu/getlistbyistipiid/{IsTipiID}")]
        [HttpGet]
        public IEnumerable<IsTipiEmirTuruDto> GetListByIsTipiID(int IsTipiID)
        {
            return _isTipiEmirTuruService.GetList(IsTipiID);
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int IsTipiID, int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _isTipiEmirTuruService.GetCountDto(IsTipiID, filter) : _isTipiEmirTuruService.GetCountDto(IsTipiID);
            var d = _isTipiEmirTuruService.GetListPaginationDto(IsTipiID, new PagingParams()
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
        public IsTipiEmirTuru Get(int id)
        {
            return _isTipiEmirTuruService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]IsTipiTemp isTipiTemp)
        {
            return _isTipiEmirTuruService.AddIsTipiDetay(isTipiTemp);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsTipiTemp isTipiTemp)
        {
            return _isTipiEmirTuruService.UpdateIsTipiDetay(isTipiTemp);
        }

        public int Delete(int id)
        {
            return _isTipiEmirTuruService.DeleteSoft(id);
        }

        [Route("api/ıstipiemirturu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isTipiEmirTuruService.Delete(id);
        }
    }
}
