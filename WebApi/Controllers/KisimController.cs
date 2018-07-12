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
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using ExcelDataReader;

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
        [Route("api/kisim/getlistbysarfyeri/{SarfYeriID}")]
        [HttpGet]
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

        [Route("api/kisim/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _kisimService.Delete(id);
        }

        [HttpPost]
        [Route("api/kisim/uploadjsonfile")]
        public HttpResponseMessage UploadJsonFile()
        {
            HttpResponseMessage response = new HttpResponseMessage();
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
                        }

                    //Dosyayı Fiziksel olarak kayıt eder.
                    postedFile.SaveAs(filePath);
                }
            }
            return response;
        }

        public void ExcelDataProcess(DataTable dataTable)
        {
            //Anahtar tablo sütun isimleri
            List<string> listColumn = new List<string>();
            foreach (var item in dataTable.Rows[0].ItemArray)
            {
                listColumn.Add(item+"");
            }

            List<Kisim> listKisim = new List<Kisim>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                //DataToObject(dataTable.Rows[i]);
                //Eklenecek veriler
                listKisim.Add(new Kisim()
                {
                    
                });
            }
        }

        //public Kisim DataToObject(object row)
        //{
        //    Kisim kisim = new Kisim();

        //    foreach (PropertyInfo propertyInfo in kisim.GetType().GetProperties())
        //    {
        //        // do stuff here
        //        propertyInfo.
        //    }
        //}
    }
}