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
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;
using System.Web;
using ExcelDataReader;
using System.Data;

namespace WebApi.Controllers
{
    public class HurdaController : ApiController
    {
        IHurdaService _hurdaService;

        public HurdaController(IHurdaService hurdaService)
        {
            _hurdaService = hurdaService;
        }

        // GET api/<controller>
        public IEnumerable<Hurda> Get()
        {
            return _hurdaService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _hurdaService.GetCount(filter) : _hurdaService.GetCount();
            var d = _hurdaService.GetListPagination(new PagingParams()
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
        public Hurda Get(int id)
        {
            return _hurdaService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Hurda hurda)
        {
            return _hurdaService.Add(hurda);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Hurda hurda)
        {
            return _hurdaService.Update(hurda);
        }

        public int Delete(int id)
        {
            return _hurdaService.DeleteSoft(id);
        }

        [Route("api/hurda/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _hurdaService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/hurda/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            Hurda hurda = new Hurda();

            PropertyInfo[] arrProp = hurda.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("Hurda");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/hurda/uploadsablonexcelfile")]
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
            List<Hurda> listHurda = new List<Hurda>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listHurda.Add(new Hurda()
                {
                    BarkodKod = row[0].ToString(),
                    VarlikID = row[1] != null ? Convert.ToInt32(row[1].ToString()) : 0,
                    OzurKod = row[2].ToString(),
                    OzurAd = row[3].ToString(),
                    OzurTip = row[4].ToString(),
                    Tarih = row[5] != null ? Convert.ToDateTime(row[5].ToString()):new DateTime(0),
                    Miktar = row[6] != null ? Convert.ToInt32(row[6].ToString()) : 0,
                    Toplam = row[7] != null ? Convert.ToInt32(row[7].ToString()) : 0,
                    Aciklama = row[8].ToString(),
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listHurdaID = _hurdaService.AddListWithTransactionBySablon(listHurda);

            return listHurdaID;
        }
    }
}