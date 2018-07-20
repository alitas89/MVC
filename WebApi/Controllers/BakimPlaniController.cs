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
using BusinessLayer.Abstract.Bakim;
using DataAccessLayer.Concrete.Dapper.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using ExcelDataReader;

namespace WebApi.Controllers
{
    public class BakimPlaniController : ApiController
    {
        IBakimPlaniService _bakimPlaniService;

        public BakimPlaniController(IBakimPlaniService bakimPlaniService)
        {
            _bakimPlaniService = bakimPlaniService;
        }

        // GET api/<controller>
        public IEnumerable<BakimPlani> Get()
        {
            return _bakimPlaniService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _bakimPlaniService.GetCount(filter) : _bakimPlaniService.GetCount();
            List<BakimPlani> d = _bakimPlaniService.GetListPagination(new PagingParams()
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
        public BakimPlani Get(int id)
        {
            return _bakimPlaniService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]BakimPlaniTemp bakimPlaniTemp)
        {
            return _bakimPlaniService.AddWithTransaction(bakimPlaniTemp.bakimPlani, bakimPlaniTemp.listIsAdimlari);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]BakimPlaniTemp bakimPlaniTemp)
        {
            return _bakimPlaniService.UpdateWithTransaction(bakimPlaniTemp.bakimPlani, bakimPlaniTemp.listIsAdimlari);
        }

        public int Delete(int id)
        {
            return _bakimPlaniService.DeleteSoft(id);
        }

        [Route("api/bakimplani/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _bakimPlaniService.Delete(id);
        }
        
        [Route("api/bakimplani/getlistbakimplanibyperiyodikbakimid/{PeriyodikBakimID}")]
        public List<BakimPlani> GetListBakimPlaniByPeriyodikBakimID(int PeriyodikBakimID)
        {
            return _bakimPlaniService.GetListBakimPlaniByPeriyodikBakimID(PeriyodikBakimID);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/bakimplani/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            BakimPlani bakimplani = new BakimPlani();

            PropertyInfo[] arrProp = bakimplani.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("BakimPlani");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/bakimplani/uploadsablonexcelfile")]
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
            List<BakimPlani> listBakimPlani = new List<BakimPlani>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listBakimPlani.Add(new BakimPlani()
                {
                    Kod = row[0].ToString(),
                    BakimPlaniTanim = row[1].ToString(),
                    ToplamBakimSuresi = row[2] != DBNull.Value ? Convert.ToInt32(row[2].ToString()) : 0,
                    ToplamIscilikSuresi = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    Aciklama = row[4].ToString(),
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listBakimPlaniID = _bakimPlaniService.AddListWithTransactionBySablon(listBakimPlani);

            return listBakimPlaniID;
        }
    }
}
