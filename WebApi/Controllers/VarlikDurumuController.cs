using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Varlik;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class VarlikDurumuController : ApiController
    {
        IVarlikDurumuService _varlikDurumuService;

        public VarlikDurumuController(IVarlikDurumuService varlikDurumuService)
        {
            _varlikDurumuService = varlikDurumuService;
        }

        // GET api/<controller>
        public IEnumerable<VarlikDurumu> Get()
        {
            return _varlikDurumuService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _varlikDurumuService.GetCount(filterCol, filterVal) : _varlikDurumuService.GetCount();
            var d = _varlikDurumuService.GetListPagination(new PagingParams()
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
        public VarlikDurumu Get(int id)
        {
            return _varlikDurumuService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]VarlikDurumu varlikDurumu)
        {
            return _varlikDurumuService.Add(varlikDurumu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]VarlikDurumu varlikDurumu)
        {
            return _varlikDurumuService.Update(varlikDurumu);
        }

        public int Delete(int id)
        {
            return _varlikDurumuService.DeleteSoft(id);
        }

        [Route("api/varlikdurumu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _varlikDurumuService.Delete(id);
        }
    }
}