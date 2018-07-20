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
using System.Data;
using DataAccessLayer.Concrete.Dapper.Varlik;
using System.Reflection;
using System.Web;
using ExcelDataReader;

namespace WebApi.Controllers
{
    public class BakimArizaKoduController : ApiController
    {
        IBakimArizaKoduService _bakimArizaKoduService;

        public BakimArizaKoduController(IBakimArizaKoduService bakimArizaKoduService)
        {
            _bakimArizaKoduService = bakimArizaKoduService;
        }

        // GET api/<controller>
        public IEnumerable<BakimArizaKodu> Get()
        {
            return _bakimArizaKoduService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _bakimArizaKoduService.GetCountDto(filter) : _bakimArizaKoduService.GetCountDto();
            var d = _bakimArizaKoduService.GetListPaginationDto(new PagingParams()
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
        public BakimArizaKodu Get(int id)
        {
            return _bakimArizaKoduService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BakimArizaKodu bakimArizaKodu)
        {
            return _bakimArizaKoduService.Add(bakimArizaKodu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BakimArizaKodu bakimArizaKodu)
        {
            return _bakimArizaKoduService.Update(bakimArizaKodu);
        }

        public int Delete(int id)
        {
            return _bakimArizaKoduService.DeleteSoft(id);
        }

        [Route("api/bakimarizakodu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bakimArizaKoduService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/bakimarizakodu/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            BakimArizaKodu bakimarizakodu = new BakimArizaKodu();

            PropertyInfo[] arrProp = bakimarizakodu.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("BakimArizaKodu");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/bakimarizakodu/uploadsablonexcelfile")]
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
            List<BakimArizaKodu> listBakimArizaKodu = new List<BakimArizaKodu>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listBakimArizaKodu.Add(new BakimArizaKodu()
                {
                    Kod = row[0].ToString(),
                    GenelKod = row[1] != DBNull.Value ? Convert.ToBoolean(row[1].ToString()) : false,
                    Ad = row[2].ToString(),
                    IsTipiID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    BakimOncelikID = row[4] != DBNull.Value ? Convert.ToInt32(row[4].ToString()) : 0,
                    TalimatKoduID = row[5] != DBNull.Value ? Convert.ToInt32(row[5].ToString()) : 0,
                    RiskTipiID = row[6] != DBNull.Value ? Convert.ToInt32(row[6].ToString()) : 0,
                    BakimPeriyodu = row[7] != DBNull.Value ? Convert.ToInt32(row[7].ToString()) : 0,
                    BirimID = row[8] != DBNull.Value ? Convert.ToInt32(row[8].ToString()) : 0,
                    BakimSuresi = row[9] != DBNull.Value ? Convert.ToInt32(row[9].ToString()) : 0,
                    BakimPuani = row[10] != DBNull.Value ? Convert.ToInt32(row[10].ToString()) : 0,
                    Etiket = row[11].ToString(),
                    SurecPerformansinaDahil = row[12] != DBNull.Value ? Convert.ToBoolean(row[12].ToString()) : false,
                    Aciklama = row[13].ToString(),
                    UretimTipiID = row[14] != DBNull.Value ? Convert.ToInt32(row[14].ToString()) : 0,
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listBakimArizaKoduID = _bakimArizaKoduService.AddListWithTransactionBySablon(listBakimArizaKodu);

            return listBakimArizaKoduID;
        }


    }
}