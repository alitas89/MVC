using System;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Security.Claims;
using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;
using System.Web;
using ExcelDataReader;
using System.Data;

namespace WebApi.Controllers
{

    public class IsTalebiController : ApiController
    {
        IIsTalebiService _isTalebiService;

        public IsTalebiController(IIsTalebiService isTalebiService)
        {
            _isTalebiService = isTalebiService;
        }

        // GET api/<controller>
        public IEnumerable<IsTalebi> Get()
        {
            return _isTalebiService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;

            //KullaniciID bilgisi alınır
            var strKullaniciID = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x =>
                x.Type.Substring(x.Type.LastIndexOf('/'), x.Type.Length - x.Type.LastIndexOf('/')) == "/nameidentifier")?.Value;
            int kullaniciID = strKullaniciID != null ? int.Parse(strKullaniciID) : 0;

            total = filter.Length != 0 ? _isTalebiService.GetCountDtoByKullaniciID(kullaniciID, filter) : _isTalebiService.GetCountDtoByKullaniciID(kullaniciID);

            var d = _isTalebiService.GetListPaginationDtoByKullaniciID(new PagingParams()
            {
                filter = filter,
                limit = limit,
                offset = offset,
                order = order,
                columns = columns
            }, kullaniciID);

            var response = Request.CreateResponse(HttpStatusCode.OK, d);
            response.Headers.Add("total", total + "");
            response.Headers.Add("Access-Control-Expose-Headers", "total");
            return response;
        }
        // GET api/<controller>/5
        public IsTalebi Get(int id)
        {
            return _isTalebiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]IsTalebi isTalebi)
        {
            return _isTalebiService.AddWithTransaction(isTalebi);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]IsTalebiIsEmriNoDto isTalebi)
        {
            return _isTalebiService.Update(isTalebi);
        }

        public int Delete(int id)
        {
            return _isTalebiService.DeleteSoft(id);
        }

        [Route("api/istalebi/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _isTalebiService.Delete(id);
        }

        [Route("api/istalebi/getistipilistbykullaniciid/{KullaniciID}")]
        public List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciID(int KullaniciID)
        {
            return _isTalebiService.GetIsTipiListByKullaniciID(KullaniciID);
        }

        [Route("api/istalebi/getemirturulistbyistipiid/{IsTipiID}")]
        public List<EmirTuruIsTipiTemp> GetEmirTuruListByIsTipiID(int IsTipiID)
        {
            return _isTalebiService.GetEmirTuruListByIsTipiID(IsTipiID);
        }

        [Route("api/istalebi/getisemrinobyistalepid/{IsTalepID}")]
        public List<IsEmriNo> GetIsEmriNoByIsTalepID(int IsTalepID)
        {
            return _isTalebiService.GetIsEmriNoByIsTalepID(IsTalepID);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/ıstalebi/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            IsTalebi ıstalebi = new IsTalebi();

            PropertyInfo[] arrProp = ıstalebi.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("IsTalebi");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/ıstalebi/uploadsablonexcelfile")]
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
            List<IsTalebi> listIsTalebi = new List<IsTalebi>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listIsTalebi.Add(new IsTalebi()
                {
                    TalepYil = row[0] != DBNull.Value ? Convert.ToInt32(row[0].ToString()) : 0,
                    IsEmriTuruID = row[1] != DBNull.Value ? Convert.ToInt32(row[1].ToString()) : 0,
                    BakimOncelikID = row[2] != DBNull.Value ? Convert.ToInt32(row[2].ToString()) : 0,
                    VarlikID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    KisimID = row[4] != DBNull.Value ? Convert.ToInt32(row[4].ToString()) : 0,
                    ArizaOlusmaTarih = row[5] != DBNull.Value ? Convert.ToDateTime(row[5].ToString()) : DateTime.MaxValue,
                    ArizaOlusmaSaat = row[6].ToString(),
                    BildirilisTarih = row[7] != DBNull.Value ? Convert.ToDateTime(row[7].ToString()) : DateTime.MaxValue,
                    BildirilisSaat = row[8].ToString(),
                    TalepEdenID = row[9] != DBNull.Value ? Convert.ToInt32(row[9].ToString()) : 0,
                    IsTipiID = row[10] != DBNull.Value ? Convert.ToInt32(row[10].ToString()) : 0,
                    BakimArizaID = row[11] != DBNull.Value ? Convert.ToInt32(row[11].ToString()) : 0,
                    Aciklama = row[12].ToString(),
                    OnaylayanID = row[13] != DBNull.Value ? Convert.ToInt32(row[13].ToString()) : 0,
                    OnaylayanAciklama = row[14].ToString(),
                    SorumluID = row[15] != DBNull.Value ? Convert.ToInt32(row[15].ToString()) : 0,
                    EkipID = row[16] != DBNull.Value ? Convert.ToInt32(row[16].ToString()) : 0,
                    OnayTarih = row[17] != DBNull.Value ? Convert.ToDateTime(row[17].ToString()) : DateTime.MaxValue,
                    OnaySaat = row[18].ToString(),
                    StatuID = row[19] != DBNull.Value ? Convert.ToInt32(row[19].ToString()) : 0,
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listIsTalebiID = _isTalebiService.AddListWithTransactionBySablon(listIsTalebi);

            return listIsTalebiID;
        }
    }
}