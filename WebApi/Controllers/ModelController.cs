﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace WebApi.Controllers
{
    public class ModelController : ApiController
    {
        IModelService _modelService;

        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }

        // GET api/<controller>
        public IEnumerable<Model> Get()
        {
            return _modelService.GetListDto();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _modelService.GetCount(filterCol, filterVal) : _modelService.GetCount();
            var d = _modelService.GetListPagination(new PagingParams()
            {
                filterCol = filterCol,
                filterVal = filterVal,
                limit = limit,
                offset = offset,
                order = order
            });
            var response = Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
        }
        // GET api/<controller>/5
        public Model Get(int id)
        {
            return _modelService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Model model)
        {
            return _modelService.Add(model);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Model model)
        {
            return _modelService.Update(model);
        }

        public int Delete(int id)
        {
            return _modelService.DeleteSoft(id);
        }

        [Route("api/model/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _modelService.Delete(id);
        }
    }
}