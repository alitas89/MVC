using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using UtilityLayer.Filters;

namespace WebApi.Controllers
{
    public class ConsumptionPlaceController : ApiController
    {
        IConsumptionPlaceService _consumptionPlaceService;

        public ConsumptionPlaceController(IConsumptionPlaceService consumptionPlaceService)
        {
            _consumptionPlaceService = consumptionPlaceService;
        }

        // GET api/<controller>
        public IEnumerable<ConsumptionPlace> Get()
        {
            return _consumptionPlaceService.GetList();
        }

        // GET api/<controller>/5
        public ConsumptionPlace Get(int id)
        {
            return _consumptionPlaceService.GetById(id);
        }

        // POST api/<controller>
        [CustomAction]
        public int Post([FromBody]ConsumptionPlace consumptionPlace)
        {
            return _consumptionPlaceService.Add(consumptionPlace);
        }

        // PUT api/<controller>/5
        [CustomAction]
        public int Put([FromBody]ConsumptionPlace consumptionPlace)
        {
            return _consumptionPlaceService.Update(consumptionPlace);
        }

        // DELETE api/<controller>/5
        [CustomAction]
        public int Delete(int id)
        {
            return _consumptionPlaceService.DeleteSoft(id);
        }

        // DELETE api/<controller>/5
        [CustomAction]
        [Route("api/consumptionplace/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _consumptionPlaceService.Delete(id);
        }
    }
}