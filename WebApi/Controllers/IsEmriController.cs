using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Linq.Dynamic;
using System.Security.Claims;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using Newtonsoft.Json;
using System.Data;
using ExcelDataReader;
using System.Web;
using DataAccessLayer.Concrete.Dapper.Varlik;
using System.Reflection;

namespace WebApi.Controllers
{
    public class IsEmriController : ApiController
    {
        IIsEmriService _isEmriService;

        public IsEmriController(IIsEmriService isEmriService)
        {
            _isEmriService = isEmriService;
        }

        // GET api/<controller>
        public IEnumerable<IsEmri> Get()
        {
            return _isEmriService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;

            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) == "/nameidentifier")?.Value;
            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;

            total = filter.Length != 0 ? _isEmriService.GetCountDtoByKullaniciID(kullaniciID, filter) : _isEmriService.GetCountDtoByKullaniciID(kullaniciID);
            var d = _isEmriService.GetListPaginationDtoByKullaniciID(new PagingParams()
            {
                filter = filter,
                limit = limit,
                offset = offset,
                order = order,
                columns = columns
            }, kullaniciID);

            var response = columns.Length > 0 ?
                Request.CreateResponse(HttpStatusCode.OK, d.Select("new(" + columns + ")").Cast<dynamic>().AsEnumerable().ToList())
                : Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
        }
        // GET api/<controller>/5
        public IsEmri Get(int id)
        {
            return _isEmriService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]IsEmri isEmri)
        {
            return _isEmriService.AddWithTransaction(isEmri);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsEmri isEmri)
        {
            return _isEmriService.Update(isEmri);
        }

        public int Delete(int id)
        {
            return _isEmriService.DeleteSoft(id);
        }

        [Route("api/isemri/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isEmriService.Delete(id);
        }

        [Route("api/isemri/getistipilistbykullaniciid/{KullaniciID}")]
        public List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciID(int KullaniciID)
        {
            return _isEmriService.GetIsTipiListByKullaniciID(KullaniciID);
        }

        [Route("api/isemri/getisemrinobyisemriid/{IsEmriID}")]
        public List<IsEmriNo> GetIsEmriNoByIsEmriID(int IsEmriID)
        {
            return _isEmriService.GetIsEmriNoByIsEmriID(IsEmriID);
        }

        [Route("api/isemri/getedityetki/{IsEmriID}")]
        public int GetEditYetki(int IsEmriID)
        {
            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) == "/nameidentifier")?.Value;
            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;

            return _isEmriService.GetEditYetki(IsEmriID, kullaniciID);
        }

        [HttpPost]
        [Route("api/isemri/addlistwithtransaction")]
        public string AddListWithTransaction(List<IsEmri> listIsemri)
        {
            return JsonConvert.SerializeObject(_isEmriService.AddListWithTransaction(listIsemri));
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/ısemri/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            IsEmri ısemri = new IsEmri();

            PropertyInfo[] arrProp = ısemri.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("IsEmri");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ısemri/uploadsablonexcelfile")]
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
            List<IsEmri> listIsEmri = new List<IsEmri>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listIsEmri.Add(new IsEmri()
                {
                    IsEmriTuruID = row[0] != DBNull.Value ? Convert.ToInt32(row[0].ToString()) : 0,
                    VarlikID = row[1] != DBNull.Value ? Convert.ToInt32(row[1].ToString()) : 0,
                    IsTipiID = row[2] != DBNull.Value ? Convert.ToInt32(row[2].ToString()) : 0,
                    BakimArizaKoduID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    BakimOncelikID = row[4] != DBNull.Value ? Convert.ToInt32(row[4].ToString()) : 0,
                    KisimID = row[5] != DBNull.Value ? Convert.ToInt32(row[5].ToString()) : 0,
                    SarfyeriID = row[6] != DBNull.Value ? Convert.ToInt32(row[6].ToString()) : 0,
                    TalepEdenID = row[7] != DBNull.Value ? Convert.ToInt32(row[7].ToString()) : 0,
                    PlanlananBaslangicTarih = row[8] != DBNull.Value ? Convert.ToDateTime(row[8].ToString()) : DateTime.MaxValue,
                    PlanlananBaslangicSaat = row[9].ToString(),
                    PlanlananBitisTarih = row[10] != DBNull.Value ? Convert.ToDateTime(row[10].ToString()) : DateTime.MaxValue,
                    PlanlananBitisSaat = row[11].ToString(),
                    ArizaOlusmaTarih = row[12] != DBNull.Value ? Convert.ToDateTime(row[12].ToString()) : DateTime.MaxValue,
                    ArizaOlusmaSaat = row[13].ToString(),
                    BildirilisTarih = row[14] != DBNull.Value ? Convert.ToDateTime(row[14].ToString()) : DateTime.MaxValue,
                    BildirilisSaat = row[15].ToString(),
                    BaslangicTarih = row[16] != DBNull.Value ? Convert.ToDateTime(row[16].ToString()) : DateTime.MaxValue,
                    BaslangicSaat = row[17].ToString(),
                    BitisTarih = row[18] != DBNull.Value ? Convert.ToDateTime(row[18].ToString()) : DateTime.MaxValue,
                    BitisSaat = row[19].ToString(),
                    DevreyeAlmaTarih = row[20] != DBNull.Value ? Convert.ToDateTime(row[20].ToString()) : DateTime.MaxValue,
                    DevreyeAlmaSaat = row[21].ToString(),
                    IsSorumluID = row[22] != DBNull.Value ? Convert.ToInt32(row[22].ToString()) : 0,
                    ArizaNedeniID = row[23] != DBNull.Value ? Convert.ToInt32(row[23].ToString()) : 0,
                    ArizaCozumuID = row[24] != DBNull.Value ? Convert.ToInt32(row[24].ToString()) : 0,
                    YapilanIsAciklama = row[25].ToString(),
                    TalepAciklamasi = row[26].ToString(),
                    StatuID = row[27] != DBNull.Value ? Convert.ToInt32(row[27].ToString()) : 0,
                    StatuAciklama = row[28].ToString(),
                    BakimEkibiID = row[29] != DBNull.Value ? Convert.ToInt32(row[29].ToString()) : 0,
                    VardiyaID = row[30] != DBNull.Value ? Convert.ToInt32(row[30].ToString()) : 0,
                    IsEmircisi = row[31] != DBNull.Value ? Convert.ToInt32(row[31].ToString()) : 0,
                    BakimDurumuID = row[32] != DBNull.Value ? Convert.ToInt32(row[32].ToString()) : 0,
                    BakimAciklamasi = row[33].ToString(),
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listIsEmriID = _isEmriService.AddListWithTransactionBySablon(listIsEmri);

            return listIsEmriID;
        }
    }
}