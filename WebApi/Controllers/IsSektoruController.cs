using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using EntityLayer.Concrete.Satinalma;
using BusinessLayer.Abstract.Satinalma;
using System.Net.Http;
using EntityLayer.ComplexTypes.ParameterModel;
using System.Net;
using System.Linq.Dynamic;

namespace WebApi.Controllers
{
    public class IsSektoruController : ApiController
    {
        IIsSektoruService _isSektoruService;

        public IsSektoruController(IIsSektoruService isSektoruService)
        {
            _isSektoruService = isSektoruService;
        }

        // GET api/<controller>
        public IEnumerable<IsSektoru> Get()
        {
            return _isSektoruService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filterCol = "", string filterVal = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filterVal.Length != 0 ? _isSektoruService.GetCount(filterCol, filterVal) : _isSektoruService.GetCount();
            var d = _isSektoruService.GetListPagination(new PagingParams()
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
        public IsSektoru Get(int id)
        {
            return _isSektoruService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]IsSektoru isSektoru)
        {
            return _isSektoruService.Add(isSektoru);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsSektoru isSektoru)
        {
            return _isSektoruService.Update(isSektoru);
        }

        public int Delete(int id)
        {
            return _isSektoruService.DeleteSoft(id);
        }

        [Route("api/issektoru/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isSektoruService.Delete(id);
        }
    }
}