using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract.Bakim;
using DataAccessLayer.Concrete.Dapper.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using ExcelDataReader;

namespace WebApi.Controllers
{
    //**WEB API CONTROLLER - PeriyodikBakim

    public class PeriyodikBakimController : ApiController
    {
        IPeriyodikBakimService _periyodikBakimService;

        public PeriyodikBakimController(IPeriyodikBakimService periyodikBakimService)
        {
            _periyodikBakimService = periyodikBakimService;
        }

        // GET api/<controller>
        public IEnumerable<PeriyodikBakim> Get()
        {
            return _periyodikBakimService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _periyodikBakimService.GetCountDto(filter) : _periyodikBakimService.GetCountDto();
            List<PeriyodikBakimDto> d = _periyodikBakimService.GetListPaginationDto(new PagingParams()
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
        public PeriyodikBakim Get(int id)
        {
            return _periyodikBakimService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]PeriyodikBakimTemp periyodikBakimTemp)
        {
            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) == "/nameidentifier")?.Value;
            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;

            return _periyodikBakimService.AddWithTransaction(periyodikBakimTemp.periyodikBakim,
                periyodikBakimTemp.listBakimPlani, periyodikBakimTemp.listBakimRiski, kullaniciID);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]PeriyodikBakimTemp periyodikBakimTemp)
        {            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) == "/nameidentifier")?.Value;
            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;

            return _periyodikBakimService.UpdateWithTransaction(periyodikBakimTemp.periyodikBakim,
                periyodikBakimTemp.listBakimPlani, periyodikBakimTemp.listBakimRiski, kullaniciID);
        }

        public int Delete(int id)
        {
            return _periyodikBakimService.DeleteSoftWithTransaction(id);
        }

        [Route("api/periyodikbakim/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _periyodikBakimService.Delete(id);
        }

        [Route("api/periyodikbakim/getlistbyvarlikid/{VarlikID}")]
        public List<PeriyodikBakim> GetListByVarlikID(int VarlikID)
        {
            return _periyodikBakimService.GetListByVarlikID(VarlikID);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/periyodikbakim/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            PeriyodikBakim periyodikbakim = new PeriyodikBakim();

            PropertyInfo[] arrProp = periyodikbakim.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("PeriyodikBakim");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/periyodikbakim/uploadsablonexcelfile")]
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
            List<PeriyodikBakim> listPeriyodikBakim = new List<PeriyodikBakim>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listPeriyodikBakim.Add(new PeriyodikBakim()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    BakimPeriyodu = row[2] != DBNull.Value ? Convert.ToInt32(row[2].ToString()) : 0,
                    PeriyodBirimID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    SonBakimTarih = row[4] != DBNull.Value ? Convert.ToDateTime(row[4].ToString()) : DateTime.MaxValue,
                    BakimYapilacakTarih = row[5] != DBNull.Value ? Convert.ToDateTime(row[5].ToString()) : DateTime.MaxValue,
                    ToleransGun = row[6] != DBNull.Value ? Convert.ToInt32(row[6].ToString()) : 0,
                    VarlikID = row[7] != DBNull.Value ? Convert.ToInt32(row[7].ToString()) : 0,
                    BakimArizaID = row[8] != DBNull.Value ? Convert.ToInt32(row[8].ToString()) : 0,
                    IsEmriTuruID = row[9] != DBNull.Value ? Convert.ToInt32(row[9].ToString()) : 0,
                    IsTipiID = row[10] != DBNull.Value ? Convert.ToInt32(row[10].ToString()) : 0,
                    KisimID = row[11] != DBNull.Value ? Convert.ToInt32(row[11].ToString()) : 0,
                    OncelikID = row[12] != DBNull.Value ? Convert.ToInt32(row[12].ToString()) : 0,
                    SorumluEkipID = row[13] != DBNull.Value ? Convert.ToInt32(row[13].ToString()) : 0,
                    IsSorumluID = row[14] != DBNull.Value ? Convert.ToInt32(row[14].ToString()) : 0,
                    ArizaNedeniID = row[15] != DBNull.Value ? Convert.ToInt32(row[15].ToString()) : 0,
                    BakimSuresi = row[16] != DBNull.Value ? Convert.ToInt32(row[16].ToString()) : 0,
                    TahminiBakimMaliyeti = row[17] != DBNull.Value ? Convert.ToDecimal(row[17].ToString()) : 0,
                    ParaBirimID = row[18] != DBNull.Value ? Convert.ToInt32(row[18].ToString()) : 0,
                    StatuID = row[19] != DBNull.Value ? Convert.ToInt32(row[19].ToString()) : 0,
                    TalepEdenID = row[20] != DBNull.Value ? Convert.ToInt32(row[20].ToString()) : 0,
                    FirmaID = row[21] != DBNull.Value ? Convert.ToInt32(row[21].ToString()) : 0,
                    TalepAciklamasi = row[22].ToString(),
                    YapilanIsinAciklamasi = row[23].ToString(),
                    IsOtomatik = row[24] != DBNull.Value ? Convert.ToBoolean(row[24].ToString()) : false,
                    IsCalismaZamaniSinirli = row[25] != DBNull.Value ? Convert.ToBoolean(row[25].ToString()) : false,
                    GecerlilikBitisTarih = row[26] != DBNull.Value ? Convert.ToDateTime(row[26].ToString()) : DateTime.MaxValue,
                    BildirimTriggerID = row[27] != DBNull.Value ? Convert.ToInt32(row[27].ToString()) : 0,
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listPeriyodikBakimID = _periyodikBakimService.AddListWithTransactionBySablon(listPeriyodikBakim);

            return listPeriyodikBakimID;
        }
    }
}