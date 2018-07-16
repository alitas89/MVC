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
using System.Web;
using ExcelDataReader;
using System.Data;
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;

namespace WebApi.Controllers
{
    public class IsletmeController : ApiController
    {
        IIsletmeService _isletmeService;

        public IsletmeController(IIsletmeService isletmeService)
        {
            _isletmeService = isletmeService;
        }

        // GET api/<controller>
        public IEnumerable<Isletme> Get()
        {
            return _isletmeService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _isletmeService.GetCount(filter) : _isletmeService.GetCount();
            var d = _isletmeService.GetListPagination(new PagingParams()
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
        public Isletme Get(int id)
        {
            return _isletmeService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Isletme isletme)
        {
            return _isletmeService.Add(isletme);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Isletme isletme)
        {
            return _isletmeService.Update(isletme);
        }

        public int Delete(int id)
        {
            return _isletmeService.DeleteSoft(id);
        }

        [Route("api/ısletme/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isletmeService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir
        [System.Web.Http.Route("api/isletme/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            Isletme ısletme = new Isletme();

            PropertyInfo[] arrProp = ısletme.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("Isletme");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }



        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/isletme/uploadsablonexcelfile")]
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
            List<Isletme> listIsletme = new List<Isletme>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listIsletme.Add(new Isletme()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    HaritaResmiYolu = row[2].ToString(),
                    Aciklama = row[3].ToString(),
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listIsletmeID = _isletmeService.AddListWithTransactionBySablon(listIsletme);

            return listIsletmeID;
        }
    }
}