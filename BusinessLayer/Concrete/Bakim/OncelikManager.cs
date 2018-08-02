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
    public class OncelikManager : IOncelikService
    {
        IOncelikDal _oncelikDal;

        public OncelikManager(IOncelikDal oncelikDal)
        {
            _oncelikDal = oncelikDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, OncelikRead, OncelikLtd")]
        public List<Oncelik> GetList()
        {
            return _oncelikDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, OncelikRead, OncelikLtd")]
        public Oncelik GetById(int Id)
        {
            return _oncelikDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, OncelikCreate")]
        public int Add(Oncelik oncelik)
        {
            return _oncelikDal.Add(oncelik);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, OncelikUpdate")]
        public int Update(Oncelik oncelik)
        {
            return _oncelikDal.Update(oncelik);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, OncelikDelete")]
        public int Delete(int Id)
        {
            return _oncelikDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, OncelikDelete")]
        public int DeleteSoft(int Id)
        {
            return _oncelikDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, OncelikRead, OncelikLtd")]
        public List<Oncelik> GetListPagination(PagingParams pagingParams)
        {
            return _oncelikDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _oncelikDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<Oncelik> listOncelik)
        {
            return _oncelikDal.AddListWithTransactionBySablon(listOncelik);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<Oncelik> ExcelDataProcess(DataTable dataTable)
        {
            List<Oncelik> listOncelik = new List<Oncelik>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listOncelik.Add(new Oncelik()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                });
            }
            return listOncelik;
        }
    }
}
