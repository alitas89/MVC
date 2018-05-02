using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;

namespace WebApi.Controllers
{

    public class IsTalebiController : ApiController
        {
            IIsTalebiService _isTalebiService;

            public IsTalebiController(IIsTalebiService isTalebiService)
            {
                _isTalebiService = isTalebiService;
            }

            // GET api/<controller>
            public IEnumerable<IsTalebi> Get()
            {
                return _isTalebiService.GetList();
            }

            // GET api/<controller>
            public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
            {
                int total = 0;
                total = filter.Length != 0 ? _isTalebiService.GetCountDto(filter) : _isTalebiService.GetCountDto();
                var d = _isTalebiService.GetListPaginationDto(new PagingParams()
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
            public IsTalebi Get(int id)
            {
                return _isTalebiService.GetById(id);
            }

            // POST api/<controller>
            public int Post([FromBody]IsTalebi isTalebi)
            {
                return _isTalebiService.Add(isTalebi);
            }

            // PUT api/<controller>/5
            public int Put([FromBody]IsTalebi isTalebi)
            {
                return _isTalebiService.Update(isTalebi);
            }

            public int Delete(int id)
            {
                return _isTalebiService.DeleteSoft(id);
            }

            [Route("api/ıstalebi/deletehard/{id}")]
            public int DeleteHard(int id)
            {
                return _isTalebiService.Delete(id);
            }

            [Route("api/ıstalebi/getistipilistbykullaniciid/{KullaniciID}")]
            public List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciID(int KullaniciID)
            {
                return _isTalebiService.GetIsTipiListByKullaniciID(KullaniciID);
            }
    }
}