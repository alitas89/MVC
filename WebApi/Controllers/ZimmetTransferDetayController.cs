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
        public HttpResponseMessage Get(int ZimmetTransferID, int offset, int limit, string filter="",
            string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0
                ? _zimmetTransferDetayService.GetCountDto(ZimmetTransferID, filter)
                : _zimmetTransferDetayService.GetCountDto(ZimmetTransferID);
            var d = _zimmetTransferDetayService.GetListPaginationDto(ZimmetTransferID, new PagingParams()
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
        public ZimmetTransferDetay Get(int id)
        {
            return _zimmetTransferDetayService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody] ZimmetTransferTemp zimmetTransferTemp)
        {
            return _zimmetTransferDetayService.AddZimmetTransferDetay(zimmetTransferTemp.ZimmetTransferID,zimmetTransferTemp.arrVarlik);
        }

        // PUT api/<controller>/5
        public int Put([FromBody] ZimmetTransferTemp zimmetTransferTemp)
        {
            return _zimmetTransferDetayService.UpdateZimmetTransferDetay(zimmetTransferTemp.ZimmetTransferID, zimmetTransferTemp.arrVarlik);
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