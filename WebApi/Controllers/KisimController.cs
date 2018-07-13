using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Varlik;
using System.Linq.Dynamic;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DataAccessLayer.Concrete.Dapper.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Others;
using ExcelDataReader;
using iTextSharp.text;

namespace WebApi.Controllers
{

    public class KisimController : ApiController
    {
        IKisimService _kisimService;

        public KisimController(IKisimService kisimService)
        {
            _kisimService = kisimService;
        }

        // GET api/<controller>
        public IEnumerable<Kisim> Get()
        {
            return _kisimService.GetListDto();
        }


        // GET api/<controller>
        [System.Web.Http.Route("api/kisim/getlistbysarfyeri/{SarfYeriID}")]
        [System.Web.Http.HttpGet]
        public IEnumerable<Kisim> GetListBySarfYeri(int SarfYeriID)
        {
            return _kisimService.GetList(SarfYeriID);
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _kisimService.GetCountDto(filter) : _kisimService.GetCountDto();
            var d = _kisimService.GetListPaginationDto(new PagingParams()
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
        public Kisim Get(int id)
        {
            return _kisimService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Kisim kisim)
        {
            return _kisimService.Add(kisim);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Kisim kisim)
        {
            return _kisimService.Update(kisim);
        }

        public int Delete(int id)
        {
            return _kisimService.DeleteSoft(id);
        }

        [System.Web.Http.Route("api/kisim/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _kisimService.Delete(id);
        }


        [System.Web.Http.Route("api/kisim/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            Kisim kisim = new Kisim();

            PropertyInfo[] arrProp = kisim.GetType().GetProperties();
            
            //İlk parça alınmaz
            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("Kisim");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/kisim/uploadsablonexcelfile")]
        public List<string> UploadSablonExcelFile()
        {
            List<string> listCreatedID = new List<string>();

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {

                    var postedFile = httpRequest.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + postedFile.FileName);

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

        public List<string> ExcelDataProcess(DataTable dataTable)
        {
            List<Kisim> listKisim = new List<Kisim>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //DataToObject(dataTable.Rows[i]);
                //Eklenecek veriler
                listKisim.Add(new Kisim()
                {
                    Kod = row[0] + "",
                    Ad = row[1] + "",
                    Butce = row[2] != null ? Convert.ToDecimal(row[2] + "") : 0,
                    HedeflenenButce = row[3] != null ? Convert.ToDecimal(row[3] + "") : 0,
                    VardiyaSinifID = row[4] != null ? Convert.ToInt32(row[4] + "") : 0,
                    SarfYeriID = row[5] != null ? Convert.ToInt32(row[5] + "") : 0,
                    Aciklama = row[6] + ""
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listKisimID = _kisimService.AddListWithTransactionBySablon(listKisim);

            return listKisimID;
        }
    }
}