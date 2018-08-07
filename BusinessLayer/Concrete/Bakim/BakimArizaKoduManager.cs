using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class BakimArizaKoduManager : IBakimArizaKoduService
    {
        IBakimArizaKoduDal _bakimarizakoduDal;

        public BakimArizaKoduManager(IBakimArizaKoduDal bakimarizakoduDal)
        {
            _bakimarizakoduDal = bakimarizakoduDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, BakimArizaKoduRead, BakimArizaKoduLtd")]
        public List<BakimArizaKodu> GetList()
        {
            return _bakimarizakoduDal.GetList();
        }
        [SecuredOperation(Roles = "Admin, BakimRead, BakimArizaKoduRead, BakimArizaKoduLtd")]
        public BakimArizaKodu GetById(int Id)
        {
            return _bakimarizakoduDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, BakimArizaKoduCreate")]
        public int Add(BakimArizaKodu bakimarizakodu)
        {
            return _bakimarizakoduDal.Add(bakimarizakodu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, BakimArizaKoduUpdate")]
        public int Update(BakimArizaKodu bakimarizakodu)
        {
            return _bakimarizakoduDal.Update(bakimarizakodu);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimArizaKoduDelete")]
        public int Delete(int Id)
        {
            return _bakimarizakoduDal.Delete(Id);
        }
        
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimArizaKoduDelete")]
        public int DeleteSoft(int Id)
        {
            return _bakimarizakoduDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BakimArizaKoduRead, BakimArizaKoduLtd")]
        public List<BakimArizaKodu> GetListPagination(PagingParams pagingParams)
        {
            return _bakimarizakoduDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _bakimarizakoduDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BakimArizaKoduRead, BakimArizaKoduLtd")]
        public List<BakimArizaKoduDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _bakimarizakoduDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _bakimarizakoduDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<BakimArizaKodu> listBakimArizaKodu)
        {
            return _bakimarizakoduDal.AddListWithTransactionBySablon(listBakimArizaKodu);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<BakimArizaKodu> ExcelDataProcess(DataTable dataTable)
        {
            List<BakimArizaKodu> listBakimArizaKodu = new List<BakimArizaKodu>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listBakimArizaKodu.Add(new BakimArizaKodu()
                {
                    Kod = row[0].ToString(),
                    GenelKod = row[1].ToString() != "" ? Convert.ToBoolean(row[1].ToString()) : false,
                    Ad = row[2].ToString(),
                    IsTipiID = row[3].ToString() != "" ? Convert.ToInt32(row[3].ToString()) : 0,
                    BakimOncelikID = row[4].ToString() != "" ? Convert.ToInt32(row[4].ToString()) : 0,
                    TalimatKoduID = row[5].ToString() != "" ? Convert.ToInt32(row[5].ToString()) : 0,
                    RiskTipiID = row[6].ToString() != "" ? Convert.ToInt32(row[6].ToString()) : 0,
                    BakimPeriyodu = row[7].ToString() != "" ? Convert.ToInt32(row[7].ToString()) : 0,
                    BirimID = row[8].ToString() != "" ? Convert.ToInt32(row[8].ToString()) : 0,
                    BakimSuresi = row[9].ToString() != "" ? Convert.ToInt32(row[9].ToString()) : 0,
                    BakimPuani = row[10].ToString() != "" ? Convert.ToInt32(row[10].ToString()) : 0,
                    Etiket = row[11].ToString(),
                    SurecPerformansinaDahil = row[12].ToString() != "" ? Convert.ToBoolean(row[12].ToString()) : false,
                    Aciklama = row[13].ToString(),
                    UretimTipiID = row[14].ToString() != "" ? Convert.ToInt32(row[14].ToString()) : 0,
                });
            }

            return listBakimArizaKodu;
        }

    }
}
