using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract.Malzeme;
using DataAccessLayer.Concrete.Dapper.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using ExcelDataReader;

namespace WebApi.Controllers
{
    public class MalzemeSayimiController : ApiController
    {
        IMalzemeSayimiService _malzemeSayimiService;

        public MalzemeSayimiController(IMalzemeSayimiService malzemeSayimiService)
        {
            _malzemeSayimiService = malzemeSayimiService;
        }

        // GET api/<controller>
        public IEnumerable<MalzemeSayimi> Get()
        {
            return _malzemeSayimiService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _malzemeSayimiService.GetCount(filter) : _malzemeSayimiService.GetCount();
            List<MalzemeSayimi> d = _malzemeSayimiService.GetListPagination(new PagingParams()
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
        public MalzemeSayimi Get(int id)
        {
            return _malzemeSayimiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]MalzemeSayimi malzemeSayimi)
        {
            return _malzemeSayimiService.Add(malzemeSayimi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]MalzemeSayimi malzemeSayimi)
        {
            return _malzemeSayimiService.Update(malzemeSayimi);
        }

        public int Delete(int id)
        {
            return _malzemeSayimiService.DeleteSoft(id);
        }

        [Route("api/malzemesayimi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _malzemeSayimiService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/malzemesayimi/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            MalzemeSayimi malzemesayimi = new MalzemeSayimi();

            PropertyInfo[] arrProp = malzemesayimi.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("MalzemeSayimi");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/malzemesayimi/uploadsablonexcelfile")]
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
                        List<MalzemeSayimi> listMalzemeSayimi = _malzemeSayimiService.ExcelDataProcess(result.Tables[0]);

                        //Transaction ile eklemeler yapılır
                       _malzemeSayimiService.AddListWithTransactionBySablon(listMalzemeSayimi);


                        //Dosyayı Fiziksel olarak kayıt eder.
                        postedFile.SaveAs(filePath);
                    }
                }
            }
            return listCreatedID;
        }

    
    }
}
