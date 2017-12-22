﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;

namespace WebApi.Controllers
{
    public class MarkaController : ApiController
    {
        IMarkaService _markaService;

        public MarkaController(IMarkaService markaService)
        {
            _markaService = markaService;
        }

        // GET api/<controller>
        public IEnumerable<Marka> Get()
        {
            return _markaService.GetList();
        }

        // GET api/<controller>/5
        public Marka Get(int id)
        {
            return _markaService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Marka marka)
        {
            return _markaService.Add(marka);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Marka marka)
        {
            return _markaService.Update(marka);
        }

        public int Delete(int id)
        {
            return _markaService.DeleteSoft(id);
        }

        [Route("api/marka/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _markaService.Delete(id);
        }
    }
}