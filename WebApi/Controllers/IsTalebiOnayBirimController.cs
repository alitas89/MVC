using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Bakim;
using BusinessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using EntityLayer.Concrete.Sistem;

namespace WebApi.Controllers
{
    public class IsTalebiOnayBirimController : ApiController
    {
        IIsTalebiOnayBirimService _isTalebiOnayBirimService;

        public IsTalebiOnayBirimController(IIsTalebiOnayBirimService isTalebiOnayBirimService)
        {
            _isTalebiOnayBirimService = isTalebiOnayBirimService;
        }

        // GET api/<controller>
        public IEnumerable<IsTalebiOnayBirim> Get()
        {
            return _isTalebiOnayBirimService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _isTalebiOnayBirimService.GetCount(filter) : _isTalebiOnayBirimService.GetCount();
            List<IsTalebiOnayBirim> d = _isTalebiOnayBirimService.GetListPagination(new PagingParams()
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
        public IsTalebiOnayBirim Get(int id)
        {
            return _isTalebiOnayBirimService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]IsTalebiOnayBirimTemp isTalebiOnayBirimTemp)
        {
            return _isTalebiOnayBirimService.AddIsTalebiOnayBirim(isTalebiOnayBirimTemp.IsTipiID, isTalebiOnayBirimTemp.arrKullaniciID);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsTalebiOnayBirim isTalebiOnayBirim)
        {
            return _isTalebiOnayBirimService.Update(isTalebiOnayBirim);
        }

        public int Delete(int id)
        {
            return _isTalebiOnayBirimService.DeleteSoft(id);
        }

        [Route("api/ıstalebionaybirim/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isTalebiOnayBirimService.Delete(id);
        }

        [Route("api/istalebionaybirim/getlistbyistipiid/{IsTipiID}")]
        public List<IsTalebiKullaniciTemp> GetListByIsTipiID(int IsTipiID)
        {
            return _isTalebiOnayBirimService.GetListByIsTipiID(IsTipiID);
        }
        
    }
}