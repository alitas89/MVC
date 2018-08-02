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
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class BakimOncelikManager : IBakimOncelikService
    {
        IBakimOncelikDal _bakimoncelikDal;

        public BakimOncelikManager(IBakimOncelikDal bakimoncelikDal)
        {
            _bakimoncelikDal = bakimoncelikDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, BakimOncelikRead, BakimOncelikLtd")]
        public List<BakimOncelik> GetList()
        {
            return _bakimoncelikDal.GetList();
        }
        [SecuredOperation(Roles = "Admin, BakimRead, BakimOncelikRead, BakimOncelikLtd")]
        public BakimOncelik GetById(int Id)
        {
            return _bakimoncelikDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, BakimOncelikCreate")]
        public int Add(BakimOncelik bakimoncelik)
        {
            return _bakimoncelikDal.Add(bakimoncelik);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, BakimOncelikUpdate")]
        public int Update(BakimOncelik bakimoncelik)
        {
            return _bakimoncelikDal.Update(bakimoncelik);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimOncelikDelete")]
        public int Delete(int Id)
        {
            return _bakimoncelikDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimOncelikDelete")]
        public int DeleteSoft(int Id)
        {
            return _bakimoncelikDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BakimOncelikRead, BakimOncelikLtd")]
        public List<BakimOncelik> GetListPagination(PagingParams pagingParams)
        {
            return _bakimoncelikDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _bakimoncelikDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<BakimOncelik> listBakimOncelik)
        {
            return _bakimoncelikDal.AddListWithTransactionBySablon(listBakimOncelik);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<BakimOncelik> ExcelDataProcess(DataTable dataTable)
        {
            List<BakimOncelik> listBakimOncelik = new List<BakimOncelik>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listBakimOncelik.Add(new BakimOncelik()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    TamamlanmaZamani = row[2] != DBNull.Value ? Convert.ToInt32(row[2].ToString()) : 0,
                    BirimID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    Aciklama = row[4].ToString(),
                    TeminSureleriID = row[5] != DBNull.Value ? Convert.ToInt32(row[5].ToString()) : 0,
                    IsEmriVarsayilani = row[6] != DBNull.Value ? Convert.ToBoolean(row[6].ToString()) : false,
                    IsTalepVarsayilani = row[7] != DBNull.Value ? Convert.ToBoolean(row[7].ToString()) : false,
                    PeriyodikBakimVarsayilani = row[8] != DBNull.Value ? Convert.ToBoolean(row[8].ToString()) : false,
                });
            }

            return listBakimOncelik;
        }
    }
}