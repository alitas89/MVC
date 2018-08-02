﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;
using System.Linq.Dynamic;
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;
using System.Web;
using ExcelDataReader;
using System.Data;

namespace WebApi.Controllers
{
    public class GonderimFormatiController : ApiController
    {
        IGonderimFormatiService _gonderimFormatiService;

        public GonderimFormatiController(IGonderimFormatiService gonderimFormatiService)
        {
            _gonderimFormatiService = gonderimFormatiService;
        }

        // GET api/<controller>
        public IEnumerable<GonderimFormati> Get()
        {
            return _gonderimFormatiService.GetList();
        }
        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _gonderimFormatiService.GetCount(filter) : _gonderimFormatiService.GetCount();
            var d = _gonderimFormatiService.GetListPagination(new PagingParams()
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
        public GonderimFormati Get(int id)
        {
            return _gonderimFormatiService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]GonderimFormati gonderimFormati)
        {
            return _gonderimFormatiService.Add(gonderimFormati);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]GonderimFormati gonderimFormati)
        {
            return _gonderimFormatiService.Update(gonderimFormati);
        }

        public int Delete(int id)
        {
            return _gonderimFormatiService.DeleteSoft(id);
        }

        [Route("api/gonderimformati/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _gonderimFormatiService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/gonderimformati/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            GonderimFormati gonderimformati = new GonderimFormati();

            PropertyInfo[] arrProp = gonderimformati.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("GonderimFormati");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/gonderimformati/uploadsablonexcelfile")]
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
                        List<GonderimFormati> listGonderimFormati = _gonderimFormatiService.ExcelDataProcess(result.Tables[0]);

                        //Transaction ile eklemeler yapılır
                       _gonderimFormatiService.AddListWithTransactionBySablon(listGonderimFormati);

                        //Dosyayı Fiziksel olarak kayıt eder.
                        postedFile.SaveAs(filePath);
                    }
                }
            }
            return listCreatedID;
        }

   
    }
}