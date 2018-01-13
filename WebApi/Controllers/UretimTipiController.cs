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
    public class UretimTipiController : ApiController
    {
        IUretimTipiService _uretimTipiService;

        public UretimTipiController(IUretimTipiService uretimTipiService)
        {
            _uretimTipiService = uretimTipiService;
        }

        // GET api/<controller>
        public IEnumerable<UretimTipi> Get()
        {
            return _uretimTipiService.GetList();
        }

        // GET api/<controller>/5
        public UretimTipi Get(int id)
        {
            return _uretimTipiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]UretimTipi uretimTipi)
        {
            return _uretimTipiService.Add(uretimTipi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]UretimTipi uretimTipi)
        {
            return _uretimTipiService.Update(uretimTipi);
        }

        public int Delete(int id)
        {
            return _uretimTipiService.DeleteSoft(id);
        }

        [Route("api/uretimtipi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _uretimTipiService.Delete(id);
        }
    }
}