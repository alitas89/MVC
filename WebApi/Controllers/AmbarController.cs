using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Linq.Dynamic;
using System;
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;
using System.Web;
using ExcelDataReader;
using System.Data;

namespace WebApi.Controllers
{
    public class AmbarController : ApiController
    {
        IAmbarService _ambarService;

        public AmbarController(IAmbarService ambarService)
        {
            _ambarService = ambarService;
        }

        // GET api/<controller>
        public IEnumerable<Ambar> Get()
        {
            return _ambarService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _ambarService.GetCountDto(filter) : _ambarService.GetCountDto();
            var d = _ambarService.GetListPaginationDto(new PagingParams()
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
        public Ambar Get(int id)
        {
            return _ambarService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Ambar ambar)
        {
            return _ambarService.Add(ambar);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Ambar ambar)
        {
            return _ambarService.Update(ambar);
        }

        public int Delete(int id)
        {
            return _ambarService.DeleteSoft(id);
        }

        [Route("api/ambar/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _ambarService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/ambar/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            Ambar ambar = new Ambar();

            PropertyInfo[] arrProp = ambar.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("Ambar");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ambar/uploadsablonexcelfile")]
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
            List<Ambar> listAmbar = new List<Ambar>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listAmbar.Add(new Ambar()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    KisimID = row[2] != DBNull.Value ? Convert.ToInt32(row[2].ToString()) : 0,
                    Aciklama = row[3].ToString(),
                    IsEmriMalzemeFiyatKatsayi = row[4] != DBNull.Value ? Convert.ToDecimal(row[4].ToString()) : 0,
                    SanalAmbarID = row[5] != DBNull.Value ? Convert.ToInt32(row[5].ToString()) : 0,
                    HurdaAmbarID = row[6] != DBNull.Value ? Convert.ToInt32(row[6].ToString()) : 0,
                    SanalAmbar = row[7] != DBNull.Value ? Convert.ToBoolean(row[7].ToString()) : false,
                    VarsayilanDeger = row[8] != DBNull.Value ? Convert.ToBoolean(row[8].ToString()) : false,
                    Semt = row[9].ToString(),
                    Sehir = row[10].ToString(),
                    Ulke = row[11].ToString(),
                    Adres = row[12].ToString(),
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listAmbarID = _ambarService.AddListWithTransactionBySablon(listAmbar);

            return listAmbarID;
        }
    }
}