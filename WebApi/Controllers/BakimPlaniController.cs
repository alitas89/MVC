using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace WebApi.Controllers
{
    public class BakimPlaniController : ApiController
    {
        IBakimPlaniService _bakimPlaniService;

        public BakimPlaniController(IBakimPlaniService bakimPlaniService)
        {
            _bakimPlaniService = bakimPlaniService;
        }

        // GET api/<controller>
        public IEnumerable<BakimPlani> Get()
        {
            return _bakimPlaniService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _bakimPlaniService.GetCount(filter) : _bakimPlaniService.GetCount();
            List<BakimPlani> d = _bakimPlaniService.GetListPagination(new PagingParams()
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
        public BakimPlani Get(int id)
        {
            return _bakimPlaniService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BakimPlaniTemp bakimPlaniTemp)
        {
            return _bakimPlaniService.AddWithTransaction(bakimPlaniTemp.bakimPlani, bakimPlaniTemp.listIsAdimlari);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BakimPlaniTemp bakimPlaniTemp)
        {
            return _bakimPlaniService.UpdateWithTransaction(bakimPlaniTemp.bakimPlani, bakimPlaniTemp.listIsAdimlari);
        }

        public int Delete(int id)
        {
            return _bakimPlaniService.DeleteSoft(id);
        }

        [Route("api/bakimplani/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bakimPlaniService.Delete(id);
        }
        
        [Route("api/bakimplani/getlistbakimplanibyperiyodikbakimid/{PeriyodikBakimID}")]
        public List<BakimPlani> GetListBakimPlaniByPeriyodikBakimID(int PeriyodikBakimID)
        {
            return _bakimPlaniService.GetListBakimPlaniByPeriyodikBakimID(PeriyodikBakimID);
        }
    }
}
