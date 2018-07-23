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
    public class MuhasebeHesapController : ApiController
    {
        IMuhasebeHesapService _muhasebeHesapService;

        public MuhasebeHesapController(IMuhasebeHesapService muhasebeHesapService)
        {
            _muhasebeHesapService = muhasebeHesapService;
        }

        // GET api/<controller>
        public IEnumerable<MuhasebeHesap> Get()
        {
            return _muhasebeHesapService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _muhasebeHesapService.GetCount(filter) : _muhasebeHesapService.GetCount();
            var d = _muhasebeHesapService.GetListPagination(new PagingParams()
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
        public MuhasebeHesap Get(int id)
        {
            return _muhasebeHesapService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]MuhasebeHesap muhasebeHesap)
        {
            return _muhasebeHesapService.Add(muhasebeHesap);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]MuhasebeHesap muhasebeHesap)
        {
            return _muhasebeHesapService.Update(muhasebeHesap);
        }

        public int Delete(int id)
        {
            return _muhasebeHesapService.DeleteSoft(id);
        }

        [Route("api/muhasebehesap/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _muhasebeHesapService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/muhasebehesap/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            MuhasebeHesap muhasebehesap = new MuhasebeHesap();

            PropertyInfo[] arrProp = muhasebehesap.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("MuhasebeHesap");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/muhasebehesap/uploadsablonexcelfile")]
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
            List<MuhasebeHesap> listMuhasebeHesap = new List<MuhasebeHesap>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listMuhasebeHesap.Add(new MuhasebeHesap()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listMuhasebeHesapID = _muhasebeHesapService.AddListWithTransactionBySablon(listMuhasebeHesap);

            return listMuhasebeHesapID;
        }
    }
}