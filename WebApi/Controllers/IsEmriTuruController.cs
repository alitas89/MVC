using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;
using System.Data;
using System;
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;
using System.Web;
using ExcelDataReader;

namespace WebApi.Controllers
{
    public class IsEmriTuruController : ApiController
    {
        IIsEmriTuruService _isEmriTuruService;

        public IsEmriTuruController(IIsEmriTuruService isEmriTuruService)
        {
            _isEmriTuruService = isEmriTuruService;
        }

        // GET api/<controller>
        public IEnumerable<IsEmriTuru> Get()
        {
            return _isEmriTuruService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _isEmriTuruService.GetCount(filter) : _isEmriTuruService.GetCount();
            var d = _isEmriTuruService.GetListPagination(new PagingParams()
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
        public IsEmriTuru Get(int id)
        {
            return _isEmriTuruService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]IsEmriTuru isEmriTuru)
        {
            return _isEmriTuruService.Add(isEmriTuru);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsEmriTuru isEmriTuru)
        {
            return _isEmriTuruService.Update(isEmriTuru);
        }

        public int Delete(int id)
        {
            return _isEmriTuruService.DeleteSoft(id);
        }

        [Route("api/ısemrituru/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isEmriTuruService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/ısemrituru/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            IsEmriTuru ısemrituru = new IsEmriTuru();

            PropertyInfo[] arrProp = ısemrituru.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("IsEmriTuru");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ısemrituru/uploadsablonexcelfile")]
        public List<string> UploadSablonExcelFile()
        {
            List<string> listCreatedID = new List<string>();

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {

                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/ " + postedFile.FileName);
                    //Gelen dosya okunur ve işleme girer.
                    using (var reader = ExcelReaderFactory.CreateReader(postedFile.InputStream))
                    {
                        // Choose one of either 1 or 2:
                        // 1. Use the reader methods
                        do
                        {
                            while (reader.Read())
                            {
                                // reader.GetDouble(0);
                            }
                        } while (reader.NextResult());

                        // 2. Use the AsDataSet extension method
                        var result = reader.AsDataSet();

                        // The result of each spreadsheet is in result.Tables
                        List<IsEmriTuru> listIsEmriTuru = _isEmriTuruService.ExcelDataProcess(result.Tables[0]);

                        //Transaction ile eklemeler yapılır
                       _isEmriTuruService.AddListWithTransactionBySablon(listIsEmriTuru);

                        //Dosyayı Fiziksel olarak kayıt eder.
                        postedFile.SaveAs(filePath);
                    }
                }
            }
            return listCreatedID;
        }

      
    }
}