﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using DataAccessLayer.Abstract.DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Dapper;
using EntityLayer.Concrete;
using Newtonsoft.Json;
using WebApi.MessageHandlers;

namespace WebApi.Controllers
{
    public class ExportController : ApiController
    {
        enum Exports
        {
            Csv,
            Pdf
        }

        private class ResultTypes
        {
            public string Version { get; set; }
            public bool Success { get; set; }
            public int StatusCode { get; set; }
            public string ErrorMessage { get; set; }
            public string ClientErrorMessage { get; set; }
            public object[] Result { get; set; }
        }

        //Enum type, string path, string token="", string data=""
        [Route("Export")]
        public HttpResponseMessage Get()
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);

            //Gerçek bir ajax işlemi gerçekleştirilir

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:54872/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/Test").Result;


            if (response.IsSuccessStatusCode)
            {
                var obj = response.Content.ReadAsAsync<ResultTypes>().Result;
                //json çevrilir ve tekrar deserialize edilir
                string jsonData = JsonConvert.SerializeObject(obj.Result);
                var list = JsonConvert.DeserializeObject<List<Test>>(jsonData);

                for (int i = 0; i < list.Count(); i++)
                {
                    //writer.Write(list[i].GetType().GetProperty());
                    PropertyInfo[] arrPropInfo = list[i].GetType().GetProperties();
                    foreach (var propInfo in arrPropInfo)
                    {
                        //writer.Write(list[i].GetType().GetProperties(propInfo));
                    }
                }
            }
            else
            {
                //"Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

            //Ajax Sonu
            writer.Write("test");
            writer.Flush();
            stream.Position = 0;

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "Export.csv" };
            return result;
        }
    }
}