using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Linq.Dynamic;

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
        [Route("api/zimmettransferdetay/getlistbyzimmettransferid/{ZimmetTransferID}")]
        [HttpGet]
        public IEnumerable<ZimmetTransferDetayDto> GetListByZimmetTransferID(int ZimmetTransferID)
        {
            return _zimmetTransferDetayService.GetList(ZimmetTransferID);
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int ZimmetTransferID, int offset, int limit, string filterCol = "", string filterVal = "",
            string order = "", string columns = "")
        {
            int total = 0;
            total = filterVal.Length != 0
                ? _zimmetTransferDetayService.GetCountDto(ZimmetTransferID, filterCol, filterVal)
                : _zimmetTransferDetayService.GetCountDto(ZimmetTransferID);
            var d = _zimmetTransferDetayService.GetListPaginationDto(ZimmetTransferID, new PagingParams()
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