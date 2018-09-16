using BusinessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Dynamic;
using System.Web;

namespace WebApi.Controllers
{
    public class DosyaController : ApiController
    {
        IDosyaService _dosyaService;

        public DosyaController(IDosyaService dosyaService)
        {
            _dosyaService = dosyaService;
        }

    

        public int GetDosyaModulID(string path)
        {
            //DosyaModul => 1-Varlik 2-İşEmri
            switch (path)
            {
                case "VarlikFiles":
                    return 1;
                case "IsEmriFiles":
                    return 2;

                default:
                    return 0;
            }
        }

        // GET api/<controller>
        [HttpGet]
        [Route("api/dosya/getlistbybagliid/{id}")]
        public List<Dosya> GetListByBagliID(int id)
        {
            return _dosyaService.GetListByBagliID(id).Where(d => d.Silindi = false).ToList();
        }

        // GET api/<controller>
        public IEnumerable<Dosya> Get()
        {
            return _dosyaService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter = "", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _dosyaService.GetCount(filter) : _dosyaService.GetCount();
            List<Dosya> d = _dosyaService.GetListPagination(new PagingParams()
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
        public Dosya Get(int id)
        {
            return _dosyaService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]Dosya dosya)
        {
            return _dosyaService.Add(dosya);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]Dosya dosya)
        {
            return _dosyaService.Update(dosya);
        }

        public int Delete(int id)
        {
            return _dosyaService.DeleteSoft(id);
        }

        [Route("api/dosya/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _dosyaService.Delete(id);
        }

        [HttpPost]
        public IHttpActionResult UploadFiles(int bagliID, string path = "")
        {
            int i = 0;
            int cntSuccess = 0;
            var uploadedFileNames = new List<string>();
            string result = string.Empty;

            HttpResponseMessage response = new HttpResponseMessage();

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[i];
                    var filePath = HttpContext.Current.Server.MapPath("~/UploadFile/" + path + "/" + postedFile.FileName);

                    try
                    {
                        postedFile.SaveAs(filePath);
                        uploadedFileNames.Add(httpRequest.Files[i].FileName);

                        //Dosya yükleme başarılı - ilgili tablolara kayıt yapılır
                        //_dosyaService.Add(new Dosya()
                        //{
                        //    Ad = postedFile.FileName,
                        //    BagliID = bagliID,
                        //    DosyaModul = GetDosyaModulID(path),
                        //    Path = filePath,
                        //    YuklenmeTarih = DateTime.Now
                        //});

                        cntSuccess++;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    i++;
                }
            }

            //Eski dosyalardan silinenler varsa veritabanından da silinir.
            //if (!String.IsNullOrEmpty(arrDeleted))
            //{
            //    var arrDeletedSplit = arrDeleted.Split(',');
            //    foreach (var deletedID in arrDeletedSplit)
            //    {
            //        if (int.TryParse(deletedID, out int parsedDeletedID))
            //        {
            //           // _dosyaService.DeleteSoft(parsedDeletedID);
            //        }
            //    }
            //}

            result = cntSuccess.ToString() + " dosya başarıyla yüklendi.<br/>";

            result += "<ul>";

            foreach (var f in uploadedFileNames)
            {
                result += "<li>" + f + "</li>";
            }

            result += "</ul>";

            return Json(result);
        }


    }
}
