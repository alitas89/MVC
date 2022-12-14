using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace WebApi.Controllers
{
    public class YetkiGrupRolController : ApiController
    {
        IYetkiGrupRolService _yetkiGrupRolService;

        public YetkiGrupRolController(IYetkiGrupRolService yetkiGrupRolService)
        {
            _yetkiGrupRolService = yetkiGrupRolService;
        }

        // GET api/<controller>
        public IEnumerable<YetkiGrupRol> Get()
        {
            return _yetkiGrupRolService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns="")
        {
            int total = 0;
            total = filter.Length != 0 ? _yetkiGrupRolService.GetCount(filter) : _yetkiGrupRolService.GetCount();
            var d = _yetkiGrupRolService.GetListPagination(new PagingParams()
            {
                filter = filter,
                limit = limit,
                offset = offset,
                order = order,
                columns=columns
            });
            var response = Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
        }
        // GET api/<controller>/5
        public YetkiGrupRol Get(int id)
        {
            return _yetkiGrupRolService.GetById(id);
        }

        // POST api/<controller>
        [Route("api/yetkigruprol/addyetkigruprol")]
        [HttpPost]
        public int Post([FromBody]YetkiGrupRolDto yetkiGrupRolDto)
        {
            return _yetkiGrupRolService.AddYetkiGrupRoles(yetkiGrupRolDto.yetkiGrupID, yetkiGrupRolDto.arrYetkiRol);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]YetkiGrupRol yetkiGrupRol)
        {
            return _yetkiGrupRolService.Update(yetkiGrupRol);
        }

        public int Delete(int id)
        {
            return _yetkiGrupRolService.DeleteSoft(id);
        }

        [Route("api/yetkigruprol/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _yetkiGrupRolService.Delete(id);
        }

        // GET api/<controller>
        [Route("api/yetkigruprol/getyetkirolbyyetkigrupid/{yetkiGrupID}")]
        [HttpGet]
        public string GetYetkiRolByYetkiGrupID(int yetkiGrupID)
        {
            return _yetkiGrupRolService.GetYetkiRolByYetkiGrupID(yetkiGrupID);
        }
    }
}