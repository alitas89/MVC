using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace WebApi.Controllers
{
    public class IsTalebiBirimController : ApiController
    {
        IIsTalebiBirimService _isTalebiBirimService;

        public IsTalebiBirimController(IIsTalebiBirimService isTalebiBirimService)
        {
            _isTalebiBirimService = isTalebiBirimService;
        }

        // GET api/<controller>
        public IEnumerable<IsTalebiBirim> Get()
        {
            return _isTalebiBirimService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _isTalebiBirimService.GetCount(filter) : _isTalebiBirimService.GetCount();
            List<IsTalebiBirim> d = _isTalebiBirimService.GetListPagination(new PagingParams()
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
        public IsTalebiBirim Get(int id)
        {
            return _isTalebiBirimService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]IsTalebiOnayBirimTemp isTalebiBirimTemp)
        {
            return _isTalebiBirimService.AddIsTalebiBirim(isTalebiBirimTemp.IsTipiID, isTalebiBirimTemp.arrKullaniciID);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsTalebiBirim isTalebiBirim)
        {
            return _isTalebiBirimService.Update(isTalebiBirim);
        }

        public int Delete(int id)
        {
            return _isTalebiBirimService.DeleteSoft(id);
        }

        [Route("api/ıstalebibirim/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isTalebiBirimService.Delete(id);
        }

        [Route("api/istalebibirim/getlistbyistipiid/{IsTipiID}")]
        public List<IsTalebiKullaniciTemp> GetListByIsTipiID(int IsTipiID)
        {
            return _isTalebiBirimService.GetListByIsTipiID(IsTipiID);
        }

    }
}