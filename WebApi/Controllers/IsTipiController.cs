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
    public class IsTipiController : ApiController
    {
        IIsTipiService _isTipiService;

        public IsTipiController(IIsTipiService isTipiService)
        {
            _isTipiService = isTipiService;
        }

        // GET api/<controller>
        public IEnumerable<IsTipi> Get()
        {
            return _isTipiService.GetListDto();
        }

        // GET api/<controller>/5
        public IsTipi Get(int id)
        {
            return _isTipiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]IsTipi isTipi)
        {
            return _isTipiService.Add(isTipi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsTipi isTipi)
        {
            return _isTipiService.Update(isTipi);
        }

        public int Delete(int id)
        {
            return _isTipiService.DeleteSoft(id);
        }

        [Route("api/ıstipi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isTipiService.Delete(id);
        }
    }
}