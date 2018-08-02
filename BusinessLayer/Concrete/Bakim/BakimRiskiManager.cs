using System;
using System.Collections.Generic;
using System.Data;
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
    public class BakimRiskiManager : IBakimRiskiService
    {
        IBakimRiskiDal _bakimriskiDal;

        public BakimRiskiManager(IBakimRiskiDal bakimriskiDal)
        {
            _bakimriskiDal = bakimriskiDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, BakimRiskiRead, BakimRiskiLtd")]
        public List<BakimRiski> GetList()
        {
            return _bakimriskiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BakimRiskiRead, BakimRiskiLtd")]
        public BakimRiski GetById(int Id)
        {
            return _bakimriskiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, BakimRiskiCreate")]
        public int Add(BakimRiski bakimriski)
        {
            return _bakimriskiDal.Add(bakimriski);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, BakimRiskiUpdate")]
        public int Update(BakimRiski bakimriski)
        {
            return _bakimriskiDal.Update(bakimriski);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimRiskiDelete")]
        public int Delete(int Id)
        {
            return _bakimriskiDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimRiskiDelete")]
        public int DeleteSoft(int Id)
        {
            return _bakimriskiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BakimRiskiRead, BakimRiskiLtd")]
        public List<BakimRiski> GetListPagination(PagingParams pagingParams)
        {
            return _bakimriskiDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _bakimriskiDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BakimRiskiRead, BakimRiskiLtd")]
        public List<BakimRiskiDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _bakimriskiDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _bakimriskiDal.GetCountDto(filter);
        }

        public List<BakimRiski> GetListBakimRiskiByPeriyodikBakimID(int PeriyodikBakimID)
        {
            return _bakimriskiDal.GetListBakimRiskiByPeriyodikBakimID(PeriyodikBakimID);
        }

        public List<string> AddListWithTransactionBySablon(List<BakimRiski> listBakimRiski)
        {
            return _bakimriskiDal.AddListWithTransactionBySablon(listBakimRiski);
        }


        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<BakimRiski> ExcelDataProcess(DataTable dataTable)
        {
            List<BakimRiski> listBakimRiski = new List<BakimRiski>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listBakimRiski.Add(new BakimRiski()
                {
                    RiskTipiID = row[0] != DBNull.Value ? Convert.ToInt32(row[0].ToString()) : 0,
                    Kod = row[1].ToString(),
                    Ad = row[2].ToString(),
                    Formulu = row[3].ToString(),
                    StokNo = row[4].ToString(),
                    Telefon = row[5].ToString(),
                    Aciklama1 = row[6].ToString(),
                    Aciklama2 = row[7].ToString(),
                    Aciklama3 = row[8].ToString(),
                });
            }



            return listBakimRiski;
        }
    }
}