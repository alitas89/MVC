using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;
using System.Linq.Dynamic;
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;
using System.Web;
using ExcelDataReader;
using System.Data;

namespace WebApi.Controllers
{
    public class HizmetController : ApiController
    {
        IHizmetService _hizmetService;

        public HizmetController(IHizmetService hizmetService)
        {
            _hizmetService = hizmetService;
        }

        // GET api/<controller>
        public IEnumerable<Hizmet> Get()
        {
            return _hizmetService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _hizmetService.GetCount(filter) : _hizmetService.GetCount();
            var d = _hizmetService.GetListPagination(new PagingParams()
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
        public Hizmet Get(int id)
        {
            return _hizmetService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Hizmet hizmet)
        {
            return _hizmetService.Add(hizmet);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Hizmet hizmet)
        {
            return _hizmetService.Update(hizmet);
        }

        public int Delete(int id)
        {
            return _hizmetService.DeleteSoft(id);
        }

        [Route("api/hizmet/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _hizmetService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/hizmet/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            Hizmet hizmet = new Hizmet();

            PropertyInfo[] arrProp = hizmet.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("Hizmet");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/hizmet/uploadsablonexcelfile")]
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
            List<Hizmet> listHizmet = new List<Hizmet>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listHizmet.Add(new Hizmet()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    BirimFiyat = row[2] != DBNull.Value ? Convert.ToDecimal(row[2].ToString()) : 0,
                    ParaBirimID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    Aciklama = row[4].ToString(),
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listHizmetID = _hizmetService.AddListWithTransactionBySablon(listHizmet);

            return listHizmetID;
        }

    }
}