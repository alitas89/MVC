using System;
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
    public class BilgilendirmeGrubuController : ApiController
    {
        IBilgilendirmeGrubuService _bilgilendirmeGrubuService;

        public BilgilendirmeGrubuController(IBilgilendirmeGrubuService bilgilendirmeGrubuService)
        {
            _bilgilendirmeGrubuService = bilgilendirmeGrubuService;
        }

        // GET api/<controller>
        public IEnumerable<BilgilendirmeGrubu> Get()
        {
            return _bilgilendirmeGrubuService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _bilgilendirmeGrubuService.GetCount(filterCol, filterVal) : _bilgilendirmeGrubuService.GetCount();
            var d = _bilgilendirmeGrubuService.GetListPagination(new PagingParams()
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
        public BilgilendirmeGrubu Get(int id)
        {
            return _bilgilendirmeGrubuService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BilgilendirmeGrubu bilgilendirmeGrubu)
        {
            return _bilgilendirmeGrubuService.Add(bilgilendirmeGrubu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BilgilendirmeGrubu bilgilendirmeGrubu)
        {
            return _bilgilendirmeGrubuService.Update(bilgilendirmeGrubu);
        }

        public int Delete(int id)
        {
            return _bilgilendirmeGrubuService.DeleteSoft(id);
        }

        [Route("api/bilgilendirmegrubu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bilgilendirmeGrubuService.Delete(id);
        }
    }
}