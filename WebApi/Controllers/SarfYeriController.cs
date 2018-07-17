using BusinessLayer.Abstract.Varlik;
using DataAccessLayer.Concrete.Dapper.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using ExcelDataReader;
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

namespace WebApi.Controllers
{
    public class SarfYeriController : ApiController
    {
        ISarfYeriService _sarfYeriService;

        public SarfYeriController(ISarfYeriService sarfYeriService)
        {
            _sarfYeriService = sarfYeriService;
        }

        // GET api/<controller>
        public IEnumerable<SarfYeriDto> Get()
        {
            return _sarfYeriService.GetListDto();
        }

        // GET api/<controller>
        [Route("api/sarfyeri/getlistbyisletme/{IsletmeID}")]
        [HttpGet]
        public IEnumerable<SarfYeri> GetListByIsletme(int IsletmeID)
        {
            return _sarfYeriService.GetList(IsletmeID);
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _sarfYeriService.GetCountDto(filter) : _sarfYeriService.GetCountDto();
            List<SarfYeriDto> d = _sarfYeriService.GetListPaginationDto(new PagingParams()
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
        public SarfYeri Get(int id)
        {
            return _sarfYeriService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]SarfYeri sarfYeri)
        {
            return _sarfYeriService.Add(sarfYeri);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]SarfYeri sarfYeri)
        {
            return _sarfYeriService.Update(sarfYeri);
        }

        public int Delete(int id)
        {
            return _sarfYeriService.DeleteSoft(id);
        }

        [Route("api/sarfyeri/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _sarfYeriService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir
        [System.Web.Http.Route("api/sarfyeri/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            SarfYeri sarfyeri = new SarfYeri();

            PropertyInfo[] arrProp = sarfyeri.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("SarfYeri");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar.
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/sarfyeri/uploadsablonexcelfile")]
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
            List<SarfYeri> listSarfYeri = new List<SarfYeri>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listSarfYeri.Add(new SarfYeri()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Butce = row[2] != null ? Convert.ToDecimal(row[2].ToString()) : 0,
                    HedeflenenButce = row[3] != null ? Convert.ToDecimal(row[3].ToString()) : 0,
                    VardiyaSinifID = row[4] != null ? Convert.ToInt32(row[4].ToString()) : 0,
                    IsletmeID = row[5] != null ? Convert.ToInt32(row[5].ToString()) : 0,
                    Telefon1 = row[6].ToString(),
                    Telefon2 = row[7].ToString(),
                    FaxNo = row[8].ToString(),
                    Email = row[9].ToString(),
                    WebUrl = row[10].ToString(),
                    LogoDosyaYolu = row[11].ToString(),
                    Aciklama = row[12].ToString(),
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listSarfYeriID = _sarfYeriService.AddListWithTransactionBySablon(listSarfYeri);

            return listSarfYeriID;
        }

    }
}