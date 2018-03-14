﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class VarlikTransferController : ApiController
    {
        IVarlikTransferService _varlikTransferService;

        public VarlikTransferController(IVarlikTransferService varlikTransferService)
        {
            _varlikTransferService = varlikTransferService;
        }

        // GET api/<controller>
        public IEnumerable<VarlikTransfer> Get()
        {
            return _varlikTransferService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _varlikTransferService.GetCountDto(filter) : _varlikTransferService.GetCountDto();
            var d = _varlikTransferService.GetListPaginationDto(new PagingParams()
            {
                filter = filter,
                limit = limit,
                offset = offset,
                order = order,
                columns = columns
            });
            var response = columns.Length > 0 ?
                Request.CreateResponse(HttpStatusCode.OK, d.Select("new(" + columns + ")").Cast<dynamic>().AsEnumerable().ToList())
                : Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
        }
        // GET api/<controller>/5
        public VarlikTransfer Get(int id)
        {
            return _varlikTransferService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]VarlikTransfer varlikTransfer)
        {
            return _varlikTransferService.Add(varlikTransfer);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]VarlikTransfer varlikTransfer)
        {
            return _varlikTransferService.Update(varlikTransfer);
        }

        public int Delete(int id)
        {
            return _varlikTransferService.DeleteSoft(id);
        }

        [Route("api/varliktransfer/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _varlikTransferService.Delete(id);
        }
    }
}