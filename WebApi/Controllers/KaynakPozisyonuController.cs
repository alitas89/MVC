using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Linq.Dynamic;
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;
using ExcelDataReader;
using System.Data;

namespace WebApi.Controllers
{
    public class KaynakPozisyonuController : ApiController
    {
        IKaynakPozisyonuService _kaynakPozisyonuService;

        public KaynakPozisyonuController(IKaynakPozisyonuService kaynakPozisyonuService)
        {
            _kaynakPozisyonuService = kaynakPozisyonuService;
        }

        // GET api/<controller>
        public IEnumerable<KaynakPozisyonu> Get()
        {
            return _kaynakPozisyonuService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _kaynakPozisyonuService.GetCount(filter) : _kaynakPozisyonuService.GetCount();
            var d = _kaynakPozisyonuService.GetListPagination(new PagingParams()
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
        public KaynakPozisyonu Get(int id)
        {
            return _kaynakPozisyonuService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]KaynakPozisyonu kaynakPozisyonu)
        {
            return _kaynakPozisyonuService.Add(kaynakPozisyonu);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]KaynakPozisyonu kaynakPozisyonu)
        {
            return _kaynakPozisyonuService.Update(kaynakPozisyonu);
        }

        public int Delete(int id)
        {
            return _kaynakPozisyonuService.DeleteSoft(id);
        }

        [Route("api/kaynakpozisyonu/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _kaynakPozisyonuService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/kaynakpozisyonu/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            KaynakPozisyonu kaynakpozisyonu = new KaynakPozisyonu();

            PropertyInfo[] arrProp = kaynakpozisyonu.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("KaynakPozisyonu");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/kaynakpozisyonu/uploadsablonexcelfile")]
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
                        List<KaynakPozisyonu> listKaynakPozisyonu = _kaynakPozisyonuService.ExcelDataProcess(result.Tables[0]);

                        //Transaction ile eklemeler yapılır
                      _kaynakPozisyonuService.AddListWithTransactionBySablon(listKaynakPozisyonu);

                        //Dosyayı Fiziksel olarak kayıt eder.
                        postedFile.SaveAs(filePath);
                    }
                }
            }
            return listCreatedID;
        }

    
    }
}