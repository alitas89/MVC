using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using BusinessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace WebApi.Controllers
{
    //**WEB API CONTROLLER - GenelBildirim

    public class GenelBildirimController : ApiController
    {
        IGenelBildirimService _genelBildirimService;

        public GenelBildirimController(IGenelBildirimService genelBildirimService)
        {
            _genelBildirimService = genelBildirimService;
        }

        // GET api/<controller>
        public IEnumerable<GenelBildirim> Get()
        {
            return _genelBildirimService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _genelBildirimService.GetCount(filter) : _genelBildirimService.GetCount();
            List<GenelBildirim> d = _genelBildirimService.GetListPagination(new PagingParams()
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
        public GenelBildirim Get(int id)
        {
            return _genelBildirimService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]GenelBildirim genelBildirim)
        {
            return _genelBildirimService.Add(genelBildirim);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]GenelBildirim genelBildirim)
        {
            return _genelBildirimService.Update(genelBildirim);
        }

        public int Delete(int id)
        {
            return _genelBildirimService.DeleteSoft(id);
        }

        [Route("api/genelbildirim/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _genelBildirimService.Delete(id);
        }

        [Route("api/genelbildirim/getlistbykime/{id}")]
        public List<GenelBildirim> GetListByKime(int id)
        {
            return _genelBildirimService.GetListByKime(id);
        }

        // GET api/<controller>
        [Route("api/genelbildirim/getlistpaginationbykime")]
        public HttpResponseMessage GetListPaginationByKime(int kullaniciID, int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _genelBildirimService.GetCountByKime(kullaniciID, filter) : _genelBildirimService.GetCountByKime(kullaniciID);
            List<GenelBildirim> d = _genelBildirimService.GetListPaginationByKime(new PagingParams()
            {
                filter = filter,
                limit = limit,
                offset = offset,
                order = order,
                columns = columns
            }, kullaniciID);
            var response = columns.Length > 0 ?
                Request.CreateResponse(HttpStatusCode.OK, d.Select("new(" + columns + ")").Cast<dynamic>().AsEnumerable().ToList())
                : Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
        }

        [Route("api/genelbildirim/getlistyenibildirimbykime/{id}")]
        public List<GenelBildirim> GetListYeniBildirimByKime(int id)
        {
            return _genelBildirimService.GetListYeniBildirimByKime(id);
        }

        [Route("api/genelbildirim/getlistgenelbildirimkullanicidtobykime/{BildirimID}")]
        public GenelBildirimKullaniciDto GetListGenelBildirimKullaniciDtoByKime(int BildirimID, int KullaniciID)
        {
            return _genelBildirimService.GetListGenelBildirimKullaniciDtoByKime(BildirimID, KullaniciID).FirstOrDefault();
        }

        [Route("api/genelbildirim/getlistgenelbildirimyoneticidtobykime/{BildirimID}")]
        public GenelBildirimYoneticiDto GetListGenelBildirimYoneticiDtoByKime(int BildirimID, int KullaniciID)
        {
            return _genelBildirimService.GetListGenelBildirimYoneticiDtoByKime(BildirimID, KullaniciID).FirstOrDefault();
        }

        [HttpPost]
        [Route("api/genelbildirim/updatepushokundu")]
        public int UpdatePushOkundu(GenelBildirimPushOkundu genelBildirimPushOkundu)
        {
            return _genelBildirimService.UpdatePushOkundu(genelBildirimPushOkundu);
        }
    }
}
