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
    public class UrunController : ApiController
    {
        IUrunService _urunService;

        public UrunController(IUrunService urunService)
        {
            _urunService = urunService;
        }

        // GET api/<controller>
        public IEnumerable<Urun> Get()
        {
            return _urunService.GetList();
        }

        // GET api/<controller>/5
        public Urun Get(int id)
        {
            return _urunService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Urun urun)
        {
            return _urunService.Add(urun);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Urun urun)
        {
            return _urunService.Update(urun);
        }

        public int Delete(int id)
        {
            return _urunService.DeleteSoft(id);
        }

        [Route("api/urun/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _urunService.Delete(id);
        }
    }
}