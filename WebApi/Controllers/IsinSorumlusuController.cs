using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class IsinSorumlusuController : ApiController
    {
        IIsinSorumlusuService _isinSorumlusuService;

        public IsinSorumlusuController(IIsinSorumlusuService isinSorumlusuService)
        {
            _isinSorumlusuService = isinSorumlusuService;
        }

        // GET api/<controller>/5
        [HttpGet]
        public IsEmriDto Get(int id)
        {
            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) == "/nameidentifier")?.Value;
            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;

            return _isinSorumlusuService.GetByKullaniciID(id,kullaniciID);
        }

        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;

            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) == "/nameidentifier")?.Value;
            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;

            total = filter.Length != 0 ? _isinSorumlusuService.GetCountDtoByKullaniciID(kullaniciID, filter) : _isinSorumlusuService.GetCountDtoByKullaniciID(kullaniciID);
            var d = _isinSorumlusuService.GetListPaginationDtoByKullaniciID(new PagingParams()
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

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsEmri isEmri)
        {
            return _isinSorumlusuService.Update(isEmri);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [Route("api/isemri/getedityetki/{IsEmriID}")]
        public int GetEditYetki(int IsEmriID)
        {
            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) == "/nameidentifier")?.Value;
            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;

            return _isinSorumlusuService.GetEditYetki(IsEmriID, kullaniciID);
        }
    }
}