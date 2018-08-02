using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract.Bakim;
using DataAccessLayer.Concrete.Dapper.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using ExcelDataReader;

namespace WebApi.Controllers
{
    //**WEB API CONTROLLER - PeriyodikBakim

    public class PeriyodikBakimController : ApiController
    {
        IPeriyodikBakimService _periyodikBakimService;

        public PeriyodikBakimController(IPeriyodikBakimService periyodikBakimService)
        {
            _periyodikBakimService = periyodikBakimService;
        }

        // GET api/<controller>
        public IEnumerable<PeriyodikBakim> Get()
        {
            return _periyodikBakimService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _periyodikBakimService.GetCountDto(filter) : _periyodikBakimService.GetCountDto();
            List<PeriyodikBakimDto> d = _periyodikBakimService.GetListPaginationDto(new PagingParams()
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
        public PeriyodikBakim Get(int id)
        {
            return _periyodikBakimService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]PeriyodikBakimTemp periyodikBakimTemp)
        {
            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) == "/nameidentifier")?.Value;
            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;

            return _periyodikBakimService.AddWithTransaction(periyodikBakimTemp.periyodikBakim,
                periyodikBakimTemp.listBakimPlani, periyodikBakimTemp.listBakimRiski, kullaniciID);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]PeriyodikBakimTemp periyodikBakimTemp)
        {            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) == "/nameidentifier")?.Value;
            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;

            return _periyodikBakimService.UpdateWithTransaction(periyodikBakimTemp.periyodikBakim,
                periyodikBakimTemp.listBakimPlani, periyodikBakimTemp.listBakimRiski, kullaniciID);
        }

        public int Delete(int id)
        {
            return _periyodikBakimService.DeleteSoftWithTransaction(id);
        }

        [Route("api/periyodikbakim/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _periyodikBakimService.Delete(id);
        }

        [Route("api/periyodikbakim/getlistbyvarlikid/{VarlikID}")]
        public List<PeriyodikBakim> GetListByVarlikID(int VarlikID)
        {
            return _periyodikBakimService.GetListByVarlikID(VarlikID);
        }
    }
}