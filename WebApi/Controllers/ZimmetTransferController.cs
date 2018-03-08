using System;
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
    public class ZimmetTransferController : ApiController
    {
        IZimmetTransferService _zimmetTransferService;

        public ZimmetTransferController(IZimmetTransferService zimmetTransferService)
        {
            _zimmetTransferService = zimmetTransferService;
        }

        // GET api/<controller>
        public IEnumerable<ZimmetTransfer> Get()
        {
            return _zimmetTransferService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _zimmetTransferService.GetCountDto(filterCol, filterVal) : _zimmetTransferService.GetCountDto();
            var d = _zimmetTransferService.GetListPaginationDto(new PagingParams()
            {
                filterCol = filterCol,
                filterVal = filterVal,
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
        public ZimmetTransfer Get(int id)
        {
            return _zimmetTransferService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]ZimmetTransfer zimmetTransfer)
        {
            return _zimmetTransferService.Add(zimmetTransfer);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]ZimmetTransfer zimmetTransfer)
        {
            return _zimmetTransferService.Update(zimmetTransfer);
        }

        public int Delete(int id)
        {
            return _zimmetTransferService.DeleteSoft(id);
        }

        [Route("api/zimmettransfer/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _zimmetTransferService.Delete(id);
        }
    }
}