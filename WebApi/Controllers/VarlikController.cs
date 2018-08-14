using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Linq.Dynamic;
using System.Data;
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;
using ExcelDataReader;

namespace WebApi.Controllers
{
    public class VarlikController : ApiController
    {
        IVarlikService _varlikService;

        public VarlikController(IVarlikService varlikService)
        {
            _varlikService = varlikService;
        }

        // GET api/<controller>
        public IEnumerable<Varlik> Get()
        {
            return _varlikService.GetList();
        }

        // GET api/<controller>
        [Route("api/varlik/getlistbykisim/{KisimID}")]
        [HttpGet]
        public IEnumerable<Varlik> GetListByKisim(int KisimID)
        {
            return _varlikService.GetListByKisimID(KisimID);
        }

        [Route("api/varlik/getlistbykaynak/{KaynakID}")]
        [HttpGet]
        public IEnumerable<Varlik> GetListByKaynak(int KaynakID)
        {
            return _varlikService.GetListByKaynakID(KaynakID);
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _varlikService.GetCountDto(filter) : _varlikService.GetCountDto();
            var d = _varlikService.GetListPaginationDto(new PagingParams()
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

        //VarlikGrubuna Göre Varlıkları Çeker
        [HttpGet]
        public HttpResponseMessage GetByVarlikGrupID(int VarlikGrupID, int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _varlikService.GetCountDtoByVarlikGrupID(VarlikGrupID, filter) : _varlikService.GetCountDtoByVarlikGrupID(VarlikGrupID);
            var d = _varlikService.GetListPaginationDtoByVarlikGrupID(VarlikGrupID, new PagingParams()
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
        public Varlik Get(int id)
        {
            return _varlikService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Varlik varlik)
        {
            return _varlikService.Add(varlik);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Varlik varlik)
        {
            return _varlikService.Update(varlik);
        }

        public int Delete(int id)
        {
            return _varlikService.DeleteSoft(id);
        }

        [Route("api/varlik/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _varlikService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/varlik/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            Varlik varlik = new Varlik();

            PropertyInfo[] arrProp = varlik.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("Varlik");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/varlik/uploadsablonexcelfile")]
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
                        List<Varlik> listVarlik = _varlikService.ExcelDataProcess(result.Tables[0]);

                        //Transaction ile eklemeler yapılır
                        _varlikService.AddListWithTransactionBySablon(listVarlik);

                        //Dosyayı Fiziksel olarak kayıt eder.
                        postedFile.SaveAs(filePath);
                    }
                }
            }
            return listCreatedID;
        }



    }
}