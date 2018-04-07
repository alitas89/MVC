using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace WebApi.Controllers
{
    public class MenuController : ApiController
    {
        IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // GET api/<controller>
        public IEnumerable<Menu> Get()
        {
            return _menuService.GetList();
        }

        [Route("api/menu/getmenubykod/{arrKodJson}")]
        public string GetMenuByKod(string arrKodJson)
        {
            return _menuService.GetMenuByKod(arrKodJson);
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _menuService.GetCount(filter) : _menuService.GetCount();
            List<Menu> d = _menuService.GetListPagination(new PagingParams()
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
        public Menu Get(int id)
        {
            return _menuService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Menu menu)
        {
            return _menuService.Add(menu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Menu menu)
        {
            return _menuService.Update(menu);
        }

        public int Delete(int id)
        {
            return _menuService.DeleteSoft(id);
        }

        [Route("api/menu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _menuService.Delete(id);
        }
    }
}
