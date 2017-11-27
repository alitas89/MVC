using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using EntityLayer.Concrete.DatabaseModel;
using EntityLayer.Concrete.RequestModel;
using UtilityLayer.Filters;

namespace WebApi.Controllers
{
    public class ProductController : ApiController
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET api/<controller>
        public IEnumerable<Product> Get()
        {
            //return _testService.GetList(1, " where Ip=@Ip", new { Ip = "Bulunamadı!" });
            return _productService.GetList();
        }

        // GET api/<controller>
        [Route("api/product/getwithcat")]
        public IEnumerable<Product> GetWithCategory()
        {
            //return _testService.GetList(1, " where Ip=@Ip", new { Ip = "Bulunamadı!" });
            return _productService.GetListWithCategory();
        }

        // GET api/<controller>
        [Route("api/product/getproductnamecolor")]
        public IEnumerable<ProductNameColorDto> GetProductNameColor()
        {
            //return _testService.GetList(1, " where Ip=@Ip", new { Ip = "Bulunamadı!" });
            var x= _productService.GetListProductNameColor();
            return x;
        }

        // GET api/<controller>
        [Route("api/product/getproductcategorynames")]
        public IEnumerable<ProductCategoryNamesDto> GetProductCategoryNames()
        {
            //return _testService.GetList(1, " where Ip=@Ip", new { Ip = "Bulunamadı!" });
            var x= _productService.GetListProductCategoryNames();
            return x;
        }

        public Product Get(int id)
        {
            Product product = _productService.GetById(id);
            return product;
        }

        // POST api/<controller>
        [CustomAction]
        public int Post([FromBody]Product product)
        {
            return _productService.Add(product);
        }
    }
}
