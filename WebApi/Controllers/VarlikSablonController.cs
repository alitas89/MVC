using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Linq.Dynamic;
using System.Web.Http;
using System.Data;
using ExcelDataReader;
using System.Web;
using DataAccessLayer.Concrete.Dapper.Varlik;
using System.Reflection;

namespace WebApi.Controllers
{
    public class VarlikSablonController : ApiController
    {
        IVarlikSablonService _varlikSablonService;

        public VarlikSablonController(IVarlikSablonService varlikSablonService)
        {
            _varlikSablonService = varlikSablonService;
        }

        // GET api/<controller>
        public IEnumerable<VarlikSablon> Get()
        {
            return _varlikSablonService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _varlikSablonService.GetCountDto(filter) : _varlikSablonService.GetCountDto();
            var d = _varlikSablonService.GetListPaginationDto(new PagingParams()
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
        public VarlikSablon Get(int id)
        {
            return _varlikSablonService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]VarlikSablon varlikSablon)
        {
            return _varlikSablonService.Add(varlikSablon);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]VarlikSablon varlikSablon)
        {
            return _varlikSablonService.Update(varlikSablon);
        }

        public int Delete(int id)
        {
            return _varlikSablonService.DeleteSoft(id);
        }

        [Route("api/varliksablon/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _varlikSablonService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/varliksablon/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            VarlikSablon varliksablon = new VarlikSablon();

            PropertyInfo[] arrProp = varliksablon.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("VarlikSablon");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/varliksablon/uploadsablonexcelfile")]
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
            List<VarlikSablon> listVarlikSablon = new List<VarlikSablon>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listVarlikSablon.Add(new VarlikSablon()
                {
                    Ad = row[0].ToString(),
                    VarlikTuruID = row[1] != DBNull.Value ? Convert.ToInt32(row[1].ToString()) : 0,
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listVarlikSablonID = _varlikSablonService.AddListWithTransactionBySablon(listVarlikSablon);

            return listVarlikSablonID;
        }


    }
}