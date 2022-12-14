using System.Collections.Generic;
using System.Data;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class GecikmeNedeniManager : IGecikmeNedeniService
    {
        IGecikmeNedeniDal _gecikmenedeniDal;

        public GecikmeNedeniManager(IGecikmeNedeniDal gecikmenedeniDal)
        {
            _gecikmenedeniDal = gecikmenedeniDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, GecikmeNedeniRead, GecikmeNedeniLtd")]
        public List<GecikmeNedeni> GetList()
        {
            return _gecikmenedeniDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, GecikmeNedeniRead, GecikmeNedeniLtd")]
        public GecikmeNedeni GetById(int Id)
        {
            return _gecikmenedeniDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, GecikmeNedeniCreate")]
        public int Add(GecikmeNedeni gecikmenedeni)
        {
            return _gecikmenedeniDal.Add(gecikmenedeni);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, GecikmeNedeniUpdate")]
        public int Update(GecikmeNedeni gecikmenedeni)
        {
            return _gecikmenedeniDal.Update(gecikmenedeni);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, GecikmeNedeniDelete")]
        public int Delete(int Id)
        {
            return _gecikmenedeniDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, GecikmeNedeniDelete")]
        public int DeleteSoft(int Id)
        {
            return _gecikmenedeniDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, GecikmeNedeniRead, GecikmeNedeniLtd")]
        public List<GecikmeNedeni> GetListPagination(PagingParams pagingParams)
        {
            return _gecikmenedeniDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _gecikmenedeniDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<GecikmeNedeni> listGecikmeNedeni)
        {
            return _gecikmenedeniDal.AddListWithTransactionBySablon(listGecikmeNedeni);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<GecikmeNedeni> ExcelDataProcess(DataTable dataTable)
        {
            List<GecikmeNedeni> listGecikmeNedeni = new List<GecikmeNedeni>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listGecikmeNedeni.Add(new GecikmeNedeni()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                });
            }

            return listGecikmeNedeni;
        }
    }
}