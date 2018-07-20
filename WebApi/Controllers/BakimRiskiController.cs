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
    public class BakimRiskiController : ApiController
    {
        IBakimRiskiService _bakimRiskiService;

        public BakimRiskiController(IBakimRiskiService bakimRiskiService)
        {
            _bakimRiskiService = bakimRiskiService;
        }

        // GET api/<controller>
        public IEnumerable<BakimRiski> Get()
        {
            return _bakimRiskiService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _bakimRiskiService.GetCountDto(filter) : _bakimRiskiService.GetCountDto();
            var d = _bakimRiskiService.GetListPaginationDto(new PagingParams()
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
        public BakimRiski Get(int id)
        {
            return _bakimRiskiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BakimRiski bakimRiski)
        {
            return _bakimRiskiService.Add(bakimRiski);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BakimRiski bakimRiski)
        {
            return _bakimRiskiService.Update(bakimRiski);
        }

        public int Delete(int id)
        {
            return _bakimRiskiService.DeleteSoft(id);
        }

        [Route("api/bakimriski/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bakimRiskiService.Delete(id);
        }

        [Route("api/bakimriski/getlistbakimriskibyperiyodikbakimid/{PeriyodikBakimID}")]
        public List<BakimRiski> GetListBakimRiskiByPeriyodikBakimID(int PeriyodikBakimID)
        {
            return _bakimRiskiService.GetListBakimRiskiByPeriyodikBakimID(PeriyodikBakimID);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/bakimriski/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            BakimRiski bakimriski = new BakimRiski();

            PropertyInfo[] arrProp = bakimriski.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("BakimRiski");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/bakimriski/uploadsablonexcelfile")]
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
            List<BakimRiski> listBakimRiski = new List<BakimRiski>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listBakimRiski.Add(new BakimRiski()
                {
                    RiskTipiID = row[0] != DBNull.Value ? Convert.ToInt32(row[0].ToString()) : 0,
                    Kod = row[1].ToString(),
                    Ad = row[2].ToString(),
                    Formulu = row[3].ToString(),
                    StokNo = row[4].ToString(),
                    Telefon = row[5].ToString(),
                    Aciklama1 = row[6].ToString(),
                    Aciklama2 = row[7].ToString(),
                    Aciklama3 = row[8].ToString(),
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listBakimRiskiID = _bakimRiskiService.AddListWithTransactionBySablon(listBakimRiski);

            return listBakimRiskiID;
        }
    }
}