using System.Collections.Generic;
using System.Data;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class RiskTipiManager : IRiskTipiService
    {
        IRiskTipiDal _risktipiDal;

        public RiskTipiManager(IRiskTipiDal risktipiDal)
        {
            _risktipiDal = risktipiDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, RiskTipiRead, RiskTipiLtd")]
        public List<RiskTipi> GetList()
        {
            return _risktipiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, RiskTipiRead, RiskTipiLtd")]
        public RiskTipi GetById(int Id)
        {
            return _risktipiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, RiskTipiCreate")]
        public int Add(RiskTipi risktipi)
        {
            return _risktipiDal.Add(risktipi);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, RiskTipiUpdate")]
        public int Update(RiskTipi risktipi)
        {
            return _risktipiDal.Update(risktipi);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, RiskTipiDelete")]
        public int Delete(int Id)
        {
            return _risktipiDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, RiskTipiDelete")]
        public int DeleteSoft(int Id)
        {
            return _risktipiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, RiskTipiRead, RiskTipiLtd")]
        public List<RiskTipi> GetListPagination(PagingParams pagingParams)
        {
            return _risktipiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _risktipiDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<RiskTipi> listRiskTipi)
        {
            return _risktipiDal.AddListWithTransactionBySablon(listRiskTipi);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<RiskTipi> ExcelDataProcess(DataTable dataTable)
        {
            List<RiskTipi> listRiskTipi = new List<RiskTipi>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listRiskTipi.Add(new RiskTipi()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                });
            }

            return listRiskTipi;
        }
    }
}
