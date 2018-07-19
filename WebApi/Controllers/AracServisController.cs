using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Linq.Dynamic;
using System;
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;
using System.Web;
using ExcelDataReader;
using System.Data;

namespace WebApi.Controllers
{
    public class AracServisController : ApiController
    {
        IAracServisService _aracServisService;

        public AracServisController(IAracServisService aracServisService)
        {
            _aracServisService = aracServisService;
        }

        // GET api/<controller>
        public IEnumerable<AracServis> Get()
        {
            return _aracServisService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _aracServisService.GetCountDto(filter) : _aracServisService.GetCountDto();
            var d = _aracServisService.GetListPaginationDto(new PagingParams()
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
        public AracServis Get(int id)
        {
            return _aracServisService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]AracServis aracServis)
        {
            return _aracServisService.Add(aracServis);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]AracServis aracServis)
        {
            return _aracServisService.Update(aracServis);
        }

        public int Delete(int id)
        {
            return _aracServisService.DeleteSoft(id);
        }

        [Route("api/aracservis/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _aracServisService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/aracservis/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            AracServis aracservis = new AracServis();

            PropertyInfo[] arrProp = aracservis.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("AracServis");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/aracservis/uploadsablonexcelfile")]
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
            List<AracServis> listAracServis = new List<AracServis>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listAracServis.Add(new AracServis()
                {
                    IsEmriYili = row[0] != DBNull.Value ? Convert.ToInt32(row[0].ToString()) : 0,
                    FisNo = row[1].ToString(),
                    TalepEdenID = row[2] != DBNull.Value ? Convert.ToInt32(row[2].ToString()) : 0,
                    AracID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    GorevID = row[4] != DBNull.Value ? Convert.ToInt32(row[4].ToString()) : 0,
                    TalepTarih = row[5] != DBNull.Value ? Convert.ToDateTime(row[5].ToString()) : DateTime.MaxValue,
                    TalepSaat = row[6].ToString(),
                    TeslimEtmeTarih = row[7] != DBNull.Value ? Convert.ToDateTime(row[7].ToString()) : DateTime.MaxValue,
                    TeslimEtmeSaat = row[8].ToString(),
                    TeslimAlmaTarih = row[9] != DBNull.Value ? Convert.ToDateTime(row[9].ToString()) : DateTime.MaxValue,
                    TeslimAlmaSaat = row[10].ToString(),
                    TeslimAlinanKm = row[11] != DBNull.Value ? Convert.ToDecimal(row[11].ToString()) : 0,
                    TeslimEdilenKm = row[12] != DBNull.Value ? Convert.ToDecimal(row[12].ToString()) : 0,
                    KullanilanKm = row[13] != DBNull.Value ? Convert.ToDecimal(row[13].ToString()) : 0,
                    Aciklama = row[14].ToString(),
                    TeslimEdenID = row[15] != DBNull.Value ? Convert.ToInt32(row[15].ToString()) : 0,
                    TeslimAlanID = row[16] != DBNull.Value ? Convert.ToInt32(row[16].ToString()) : 0,
                    TeslimAmbarID = row[17] != DBNull.Value ? Convert.ToInt32(row[17].ToString()) : 0,
                    BolumID = row[18] != DBNull.Value ? Convert.ToInt32(row[18].ToString()) : 0,
                    VarlikDurumID = row[19] != DBNull.Value ? Convert.ToInt32(row[19].ToString()) : 0,
                    MarkaID = row[20] != DBNull.Value ? Convert.ToInt32(row[20].ToString()) : 0,
                    ModelID = row[21] != DBNull.Value ? Convert.ToInt32(row[21].ToString()) : 0,
                    SeriNo = row[22].ToString(),
                    ArizaID = row[23] != DBNull.Value ? Convert.ToInt32(row[23].ToString()) : 0,
                    HizmetID = row[24] != DBNull.Value ? Convert.ToInt32(row[24].ToString()) : 0,
                    ServisAdres = row[25].ToString(),
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listAracServisID = _aracServisService.AddListWithTransactionBySablon(listAracServis);

            return listAracServisID;
        }
    }
}