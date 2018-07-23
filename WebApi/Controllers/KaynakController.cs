using BusinessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;
using System;
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;
using System.Web;
using ExcelDataReader;
using System.Data;

namespace WebApi.Controllers
{
    public class KaynakController : ApiController
    {
        IKaynakService _kaynakService;

        public KaynakController(IKaynakService kaynakService)
        {
            _kaynakService = kaynakService;
        }

        // GET api/<controller>
        public IEnumerable<Kaynak> Get()
        {
            return _kaynakService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _kaynakService.GetCountDto(filter) : _kaynakService.GetCountDto();
            var d = _kaynakService.GetListPaginationDto(new PagingParams()
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
        public Kaynak Get(int id)
        {
            return _kaynakService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Kaynak kaynak)
        {
            return _kaynakService.Add(kaynak);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Kaynak kaynak)
        {
            return _kaynakService.Update(kaynak);
        }

        public int Delete(int id)
        {
            return _kaynakService.DeleteSoft(id);
        }

        [Route("api/kaynak/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _kaynakService.Delete(id);
        }


        [Route("api/kaynak/getlistkaynakhavekullaniciid")]
        public List<Kaynak> GetListKaynakHaveKullaniciID()
        {
            return _kaynakService.GetListKaynakHaveKullaniciID();
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/kaynak/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            Kaynak kaynak = new Kaynak();

            PropertyInfo[] arrProp = kaynak.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("Kaynak");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/kaynak/uploadsablonexcelfile")]
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
            List<Kaynak> listKaynak = new List<Kaynak>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listKaynak.Add(new Kaynak()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    KisimID = row[2] != DBNull.Value ? Convert.ToInt32(row[2].ToString()) : 0,
                    SarfyeriID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    IsletmeID = row[4] != DBNull.Value ? Convert.ToInt32(row[4].ToString()) : 0,
                    VarlikID = row[5] != DBNull.Value ? Convert.ToInt32(row[5].ToString()) : 0,
                    EkipID = row[6] != DBNull.Value ? Convert.ToInt32(row[6].ToString()) : 0,
                    KaynakSinifID = row[7] != DBNull.Value ? Convert.ToInt32(row[7].ToString()) : 0,
                    VardiyaID = row[8] != DBNull.Value ? Convert.ToInt32(row[8].ToString()) : 0,
                    StatuID = row[9] != DBNull.Value ? Convert.ToInt32(row[9].ToString()) : 0,
                    AmbarID = row[10] != DBNull.Value ? Convert.ToInt32(row[10].ToString()) : 0,
                    KaynakPozisyonuID = row[11] != DBNull.Value ? Convert.ToInt32(row[11].ToString()) : 0,
                    DurusIsTipiID = row[12] != DBNull.Value ? Convert.ToInt32(row[12].ToString()) : 0,
                    KaynakTipiID = row[13] != DBNull.Value ? Convert.ToInt32(row[13].ToString()) : 0,
                    KaynakDurumuID = row[14] != DBNull.Value ? Convert.ToInt32(row[14].ToString()) : 0,
                    KaynakTuruID = row[15] != DBNull.Value ? Convert.ToInt32(row[15].ToString()) : 0,
                    Email = row[16].ToString(),
                    TelefonNo = row[17].ToString(),
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listKaynakID = _kaynakService.AddListWithTransactionBySablon(listKaynak);

            return listKaynakID;
        }
    }
}