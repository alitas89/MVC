using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace WebApi.Controllers
{
    public class ZimmetTransferDetayController : ApiController
    {
        IZimmetTransferDetayService _zimmetTransferDetayService;

        public ZimmetTransferDetayController(IZimmetTransferDetayService zimmetTransferDetayService)
        {
            _zimmetTransferDetayService = zimmetTransferDetayService;
        }

        // GET api/<controller>
        public IEnumerable<ZimmetTransferDetay> Get()
        {
            return _zimmetTransferDetayService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "",
            string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0
                ? _zimmetTransferDetayService.GetCount(filterCol, filterVal)
                : _zimmetTransferDetayService.GetCount();
            var d = _zimmetTransferDetayService.GetListPagination(new PagingParams()
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
        public ZimmetTransferDetay Get(int id)
        {
            return _zimmetTransferDetayService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody] ZimmetTransferDetay zimmetTransferDetay)
        {
            return _zimmetTransferDetayService.Add(zimmetTransferDetay);
        }

        // PUT api/<controller>/5
        public int Put([FromBody] ZimmetTransferDetay zimmetTransferDetay)
        {
            return _zimmetTransferDetayService.Update(zimmetTransferDetay);
        }

        public int Delete(int id)
        {
            return _zimmetTransferDetayService.DeleteSoft(id);
        }

        [Route("api/zimmettransferdetay/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _zimmetTransferDetayService.Delete(id);
        }
    }
}