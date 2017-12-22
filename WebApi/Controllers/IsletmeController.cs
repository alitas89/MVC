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
    public class IsletmeController : ApiController
    {
            IIsletmeService _isletmeService;

            public IsletmeController(IIsletmeService isletmeService)
            {
                _isletmeService = isletmeService;
            }

            // GET api/<controller>
            public IEnumerable<Isletme> Get()
            {
                return _isletmeService.GetList();
            }

            // GET api/<controller>/5
            public Isletme Get(int id)
            {
                return _isletmeService.GetById(id);
            }

            // POST api/<controller>
            public int Post([FromBody]Isletme isletme)
            {
                return _isletmeService.Add(isletme);
            }

            // PUT api/<controller>/5
            public int Put([FromBody]Isletme isletme)
            {
                return _isletmeService.Update(isletme);
            }

            public int Delete(int id)
            {
                return _isletmeService.DeleteSoft(id);
            }

            [Route("api/ısletme/deletehard/{id}")]
            public int DeleteHard(int id)
            {
                return _isletmeService.Delete(id);
            }
        }
}