using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Linq.Dynamic;
using System.Security.Claims;
using EntityLayer.ComplexTypes.DtoModel.Bakim;

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

            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) == "/nameidentifier")?.Value;
            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;

            total = filter.Length != 0 ? _isEmriService.GetCountDtoByKullaniciID(kullaniciID, filter) : _isEmriService.GetCountDtoByKullaniciID(kullaniciID);
            var d = _isEmriService.GetListPaginationDtoByKullaniciID(new PagingParams()
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

        [Route("api/isemri/getistipilistbykullaniciid/{KullaniciID}")]
        public List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciID(int KullaniciID)
        {
            return _isEmriService.GetIsTipiListByKullaniciID(KullaniciID);
        }
        
    }
}