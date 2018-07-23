using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Linq.Dynamic;
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;
using ExcelDataReader;
using System.Data;

namespace WebApi.Controllers
{
    public class VardiyaSinifiController : ApiController
    {
        IVardiyaSinifiService _vardiyaSinifiService;

        public VardiyaSinifiController(IVardiyaSinifiService vardiyaSinifiService)
        {
            _vardiyaSinifiService = vardiyaSinifiService;
        }

        // GET api/<controller>
        public IEnumerable<VardiyaSinifi> Get()
        {
            return _vardiyaSinifiService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _vardiyaSinifiService.GetCount(filter) : _vardiyaSinifiService.GetCount();
            var d = _vardiyaSinifiService.GetListPagination(new PagingParams()
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
        public VardiyaSinifi Get(int id)
        {
            return _vardiyaSinifiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]VardiyaSinifi vardiyaSinifi)
        {
            return _vardiyaSinifiService.Add(vardiyaSinifi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]VardiyaSinifi vardiyaSinifi)
        {
            return _vardiyaSinifiService.Update(vardiyaSinifi);
        }

        public int Delete(int id)
        {
            return _vardiyaSinifiService.DeleteSoft(id);
        }

        [Route("api/vardiyasinifi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _vardiyaSinifiService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/vardiyasinifi/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            VardiyaSinifi vardiyasinifi = new VardiyaSinifi();

            PropertyInfo[] arrProp = vardiyasinifi.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("VardiyaSinifi");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/vardiyasinifi/uploadsablonexcelfile")]
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
                        ExcelDataProcess(result.Tables[0]);

                        //Dosyayı Fiziksel olarak kayıt eder.
                        postedFile.SaveAs(filePath);
                    }
                }
            }
            return listCreatedID;
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<string> ExcelDataProcess(DataTable dataTable)
        {
            List<VardiyaSinifi> listVardiyaSinifi = new List<VardiyaSinifi>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listVardiyaSinifi.Add(new VardiyaSinifi()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listVardiyaSinifiID = _vardiyaSinifiService.AddListWithTransactionBySablon(listVardiyaSinifi);

            return listVardiyaSinifiID;
        }

    }
}