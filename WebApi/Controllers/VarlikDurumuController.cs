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
    public class VarlikDurumuController : ApiController
    {
        IVarlikDurumuService _varlikDurumuService;

        public VarlikDurumuController(IVarlikDurumuService varlikDurumuService)
        {
            _varlikDurumuService = varlikDurumuService;
        }

        // GET api/<controller>
        public IEnumerable<VarlikDurumu> Get()
        {
            return _varlikDurumuService.GetList();
        }

        // GET api/<controller>/5
        public VarlikDurumu Get(int id)
        {
            return _varlikDurumuService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]VarlikDurumu varlikDurumu)
        {
            return _varlikDurumuService.Add(varlikDurumu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]VarlikDurumu varlikDurumu)
        {
            return _varlikDurumuService.Update(varlikDurumu);
        }

        public int Delete(int id)
        {
            return _varlikDurumuService.DeleteSoft(id);
        }

        [Route("api/varlikdurumu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _varlikDurumuService.Delete(id);
        }
    }
}