using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;

namespace WebApi.Controllers
{
    public class VarlikGrupController : ApiController
    {
        IVarlikGrupService _varlikGrupService;

        public VarlikGrupController(IVarlikGrupService varlikGrupService)
        {
            _varlikGrupService = varlikGrupService;
        }

        // GET api/<controller>
        public IEnumerable<VarlikGrup> Get()
        {
            return _varlikGrupService.GetListDto();
        }

        // GET api/<controller>/5
        public VarlikGrup Get(int id)
        {
            return _varlikGrupService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]VarlikGrup varlikGrup)
        {
            return _varlikGrupService.Add(varlikGrup);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]VarlikGrup varlikGrup)
        {
            return _varlikGrupService.Update(varlikGrup);
        }

        public int Delete(int id)
        {
            return _varlikGrupService.DeleteSoft(id);
        }

        [Route("api/varlikgrup/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _varlikGrupService.Delete(id);
        }
    }
}