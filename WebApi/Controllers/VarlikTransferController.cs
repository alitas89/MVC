using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Linq.Dynamic;
using System.Reflection;
using DataAccessLayer.Concrete.Dapper.Varlik;
using System.Web;
using ExcelDataReader;
using System.Data;

namespace WebApi.Controllers
{
    public class VarlikTransferController : ApiController
    {
        IVarlikTransferService _varlikTransferService;

        public VarlikTransferController(IVarlikTransferService varlikTransferService)
        {
            _varlikTransferService = varlikTransferService;
        }

        // GET api/<controller>
        public IEnumerable<VarlikTransfer> Get()
        {
            return _varlikTransferService.GetList();
        }

        // GET api/<controller>
        public HttpResponseMessage Get(int offset, int limit, string filter="", string order = "", string columns = "")
        {
            int total = 0;
            total = filter.Length != 0 ? _varlikTransferService.GetCountDto(filter) : _varlikTransferService.GetCountDto();
            var d = _varlikTransferService.GetListPaginationDto(new PagingParams()
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
        public VarlikTransfer Get(int id)
        {
            return _varlikTransferService.GetById(id);
        }

        // POST api/<controller>
        public int Post([FromBody]VarlikTransfer varlikTransfer)
        {
            return _varlikTransferService.Add(varlikTransfer);
        }

        // PUT api/<controller>/5
        public int Put([FromBody]VarlikTransfer varlikTransfer)
        {
            return _varlikTransferService.Update(varlikTransfer);
        }

        public int Delete(int id)
        {
            return _varlikTransferService.DeleteSoft(id);
        }

        [Route("api/varliktransfer/deletehard/{id}")]
        public int DeleteHard(int id)
        {
            return _varlikTransferService.Delete(id);
        }

        //*Boş şablon hazırlar ve yüklenmesine izin verir 
        [System.Web.Http.Route("api/varliktransfer/downloadsablon")]
        public HttpResponseMessage GetExcelSablon()
        {
            List<String> list = new List<String>();
            List<Type> listType = new List<Type>();
            VarlikTransfer varliktransfer = new VarlikTransfer();

            PropertyInfo[] arrProp = varliktransfer.GetType().GetProperties();

            for (int i = 1; i < arrProp.Length; i++)
            {
                list.Add(arrProp[i].Name);
                listType.Add(typeof(string));
            }

            MyClassBuilder MCB = new MyClassBuilder("VarlikTransfer");
            var myclass = MCB.CreateObject(list.ToArray(), listType.ToArray());

            return Request.CreateResponse(HttpStatusCode.OK, myclass);
        }

        //*İçerisinde kayıtların olduğu bir excel dosyası hazırlar ve upload edilmesini sağlar. 
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/varliktransfer/uploadsablonexcelfile")]
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
            List<VarlikTransfer> listVarlikTransfer = new List<VarlikTransfer>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listVarlikTransfer.Add(new VarlikTransfer()
                {
                    TransferNo = row[0] != DBNull.Value ? Convert.ToInt32(row[0].ToString()) : 0,
                    VarlikID = row[1] != DBNull.Value ? Convert.ToInt32(row[1].ToString()) : 0,
                    EskiKisimID = row[2] != DBNull.Value ? Convert.ToInt32(row[2].ToString()) : 0,
                    EskiSahipVarlikID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    YeniSahipVarlikID = row[4] != DBNull.Value ? Convert.ToInt32(row[4].ToString()) : 0,
                    YeniKisimID = row[5] != DBNull.Value ? Convert.ToInt32(row[5].ToString()) : 0,
                    IslemiYapanID = row[6] != DBNull.Value ? Convert.ToInt32(row[6].ToString()) : 0,
                    Tarih = row[7] != DBNull.Value ? Convert.ToDateTime(row[7].ToString()) : DateTime.MaxValue,
                    Saat = row[8].ToString(),
                    Aciklama = row[9].ToString(),
                });
            }

            //Transaction ile eklemeler yapılır
            List<string> listVarlikTransferID = _varlikTransferService.AddListWithTransactionBySablon(listVarlikTransfer);

            return listVarlikTransferID;
        }
    }
}