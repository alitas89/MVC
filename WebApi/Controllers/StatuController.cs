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
    public class StatuController : ApiController
    {
        IStatuService _statuService;

        public StatuController(IStatuService statuService)
        {
            _statuService = statuService;
        }

        // GET api/<controller>
        public IEnumerable<Statu> Get()
        {
            return _statuService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _statuService.GetCountDto(filter) : _statuService.GetCountDto();
            var d = _statuService.GetListPaginationDto(new PagingParams()
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
        public Statu Get(int id)
        {
            return _statuService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Statu statu)
        {
            return _statuService.Add(statu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Statu statu)
        {
            return _statuService.Update(statu);
        }

        public int Delete(int id)
        {
            return _statuService.DeleteSoft(id);
        }

        [Route("api/statu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _statuService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/statu/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            Statu statu = new Statu();

            PropertyInfo[] arrProp = statu.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("Statu");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/statu/uploadsablonexcelfile")]
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
            List<Statu> listStatu = new List<Statu>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listStatu.Add(new Statu()
                {
                    StatuTipiID = row[0] != DBNull.Value ? Convert.ToInt32(row[0].ToString()) : 0,
                    Kod = row[1].ToString(),
                    Ad = row[2].ToString(),
                    VarlikDurumuID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    KaynakSinifi1ID = row[4] != DBNull.Value ? Convert.ToInt32(row[4].ToString()) : 0,
                    KaynakSinifi2ID = row[5] != DBNull.Value ? Convert.ToInt32(row[5].ToString()) : 0,
                    KaynakSinifi3ID = row[6] != DBNull.Value ? Convert.ToInt32(row[6].ToString()) : 0,
                    Aciklama = row[7].ToString(),
                    BeklemeIptalNedeni = row[8] != DBNull.Value ? Convert.ToBoolean(row[8].ToString()) : false,
                    TalepVarsayilani = row[9] != DBNull.Value ? Convert.ToBoolean(row[9].ToString()) : false,
                    TalepOnay = row[10] != DBNull.Value ? Convert.ToBoolean(row[10].ToString()) : false,
                    TalepRed = row[11] != DBNull.Value ? Convert.ToBoolean(row[11].ToString()) : false,
                    EmirVarsayilani = row[12] != DBNull.Value ? Convert.ToBoolean(row[12].ToString()) : false,
                    IsEmriAcik = row[13] != DBNull.Value ? Convert.ToBoolean(row[13].ToString()) : false,
                    IsEmriKapali = row[14] != DBNull.Value ? Convert.ToBoolean(row[14].ToString()) : false,
                    IsEmriIptal = row[15] != DBNull.Value ? Convert.ToBoolean(row[15].ToString()) : false,
                    EkipmanCalismiyor = row[16] != DBNull.Value ? Convert.ToBoolean(row[16].ToString()) : false,
                    PlanlanmisIsEmri = row[17] != DBNull.Value ? Convert.ToBoolean(row[17].ToString()) : false,
                    IsEmrineBaslandi = row[18] != DBNull.Value ? Convert.ToBoolean(row[18].ToString()) : false,
                    IsEmriTamamlandi = row[19] != DBNull.Value ? Convert.ToBoolean(row[19].ToString()) : false,
                    IsTeslimEdildi = row[20] != DBNull.Value ? Convert.ToBoolean(row[20].ToString()) : false,
                    SorumluDegisti = row[21] != DBNull.Value ? Convert.ToBoolean(row[21].ToString()) : false,
                    BakimErtelendi = row[22] != DBNull.Value ? Convert.ToBoolean(row[22].ToString()) : false,
                    BakimDevamEdiyor = row[23] != DBNull.Value ? Convert.ToBoolean(row[23].ToString()) : false,
                    BildirimIslemleriniYoksay = row[24] != DBNull.Value ? Convert.ToBoolean(row[24].ToString()) : false,
                    KismiSatinalmaTalebiOlusturuldu = row[25] != DBNull.Value ? Convert.ToBoolean(row[25].ToString()) : false,
                    SatinalmaTalebiOlusturuldu = row[26] != DBNull.Value ? Convert.ToBoolean(row[26].ToString()) : false,
                    SatinalmaTeklifVerildi = row[27] != DBNull.Value ? Convert.ToBoolean(row[27].ToString()) : false,
                    SatinalmaTeklifDegerlendirildi = row[28] != DBNull.Value ? Convert.ToBoolean(row[28].ToString()) : false,
                    SatinalmaSiparisVerildi = row[29] != DBNull.Value ? Convert.ToBoolean(row[29].ToString()) : false,
                    MalzemelerinSatinalmaFiyatBelirlendi = row[30] != DBNull.Value ? Convert.ToBoolean(row[30].ToString()) : false,
                    SatinalmaAmbarGirisiYapildi = row[31] != DBNull.Value ? Convert.ToBoolean(row[31].ToString()) : false,
                    EpostaGonder = row[32] != DBNull.Value ? Convert.ToBoolean(row[32].ToString()) : false,
                    SMSGonder = row[33] != DBNull.Value ? Convert.ToBoolean(row[33].ToString()) : false,
                    HerKayitAsamasindaUygula = row[34] != DBNull.Value ? Convert.ToBoolean(row[34].ToString()) : false,
                    KaydiKilitle = row[35] != DBNull.Value ? Convert.ToBoolean(row[35].ToString()) : false,
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listStatuID = _statuService.AddListWithTransactionBySablon(listStatu);

            return listStatuID;
        }
    }
}