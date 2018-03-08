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
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _modelService.GetCountDto(filterCol, filterVal) : _modelService.GetCountDto();
            var d = _modelService.GetListPaginationDto(new PagingParams()
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