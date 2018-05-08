using System;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Security.Claims;
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

            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) == "/nameidentifier")?.Value;
            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;

            total = filter.Length != 0 ? _isTalebiService.GetCountDtoByKullaniciID(kullaniciID, filter) : _isTalebiService.GetCountDtoByKullaniciID(kullaniciID);

            var d = _isTalebiService.GetListPaginationDtoByKullaniciID(new PagingParams()
            {
                filter = filter,
                limit = limit,
                offset = offset,
                order = order,
                columns = columns
            }, kullaniciID);

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
            return _isTalebiService.AddWithTransaction(isTalebi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsTalebi isTalebi, int IsEmriNoID)
        {
            return _isTalebiService.Update(isTalebi, IsEmriNoID);
        }

        public int Delete(int id)
        {
            return _isTalebiService.DeleteSoft(id);
        }

        [Route("api/istalebi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isTalebiService.Delete(id);
        }

        [Route("api/istalebi/getistipilistbykullaniciid/{KullaniciID}")]
        public List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciID(int KullaniciID)
        {
            return _isTalebiService.GetIsTipiListByKullaniciID(KullaniciID);
        }

        [Route("api/istalebi/getemirturulistbyistipiid/{IsTipiID}")]
        public List<EmirTuruIsTipiTemp> GetEmirTuruListByIsTipiID(int IsTipiID)
        {
            return _isTalebiService.GetEmirTuruListByIsTipiID(IsTipiID);
        }

        [Route("api/istalebi/getisemrinobyistalepid/{IsTalepID}")]
        public List<IsEmriNo> GetIsEmriNoByIsTalepID(int IsTalepID)
        {
            return _isTalebiService.GetIsEmriNoByIsTalepID(IsTalepID);
        }
    }
}