using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Linq.Dynamic;
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;
using ExcelDataReader;
using System.Data;

namespace WebApi.Controllers
{
    public class MalzemeAltGrupController : ApiController
    {
        IMalzemeAltGrupService _malzemeAltGrupService;

        public MalzemeAltGrupController(IMalzemeAltGrupService malzemeAltGrupService)
        {
            _malzemeAltGrupService = malzemeAltGrupService;
        }

        // GET api/<controller>
        public IEnumerable<MalzemeAltGrup> Get()
        {
            return _malzemeAltGrupService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _malzemeAltGrupService.GetCount(filter) : _malzemeAltGrupService.GetCount();
            var d = _malzemeAltGrupService.GetListPagination(new PagingParams()
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
        public MalzemeAltGrup Get(int id)
        {
            return _malzemeAltGrupService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]MalzemeAltGrup malzemeAltGrup)
        {
            return _malzemeAltGrupService.Add(malzemeAltGrup);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]MalzemeAltGrup malzemeAltGrup)
        {
            return _malzemeAltGrupService.Update(malzemeAltGrup);
        }

        public int Delete(int id)
        {
            return _malzemeAltGrupService.DeleteSoft(id);
        }

        [Route("api/malzemealtgrup/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _malzemeAltGrupService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/malzemealtgrup/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            MalzemeAltGrup malzemealtgrup = new MalzemeAltGrup();

            PropertyInfo[] arrProp = malzemealtgrup.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("MalzemeAltGrup");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/malzemealtgrup/uploadsablonexcelfile")]
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
                        List<MalzemeAltGrup> listMalzemeAltGrup = _malzemeAltGrupService.ExcelDataProcess(result.Tables[0]);

                        //Transaction ile eklemeler yapılır
                        _malzemeAltGrupService.AddListWithTransactionBySablon(listMalzemeAltGrup);

                        //Dosyayı Fiziksel olarak kayıt eder.
                        postedFile.SaveAs(filePath);
                    }
                }
            }
            return listCreatedID;
        }

    }
}