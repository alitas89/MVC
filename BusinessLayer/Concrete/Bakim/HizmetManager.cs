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
    public class HizmetManager : IHizmetService
    {
        IHizmetDal _hizmetDal;

        public HizmetManager(IHizmetDal hizmetDal)
        {
            _hizmetDal = hizmetDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, HizmetRead, HizmetLtd")]
        public List<Hizmet> GetList()
        {
            return _hizmetDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, HizmetRead, HizmetLtd")]
        public Hizmet GetById(int Id)
        {
            return _hizmetDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, HizmetCreate")]
        public int Add(Hizmet hizmet)
        {
            return _hizmetDal.Add(hizmet);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, HizmetUpdate")]
        public int Update(Hizmet hizmet)
        {
            return _hizmetDal.Update(hizmet);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, HizmetDelete")]
        public int Delete(int Id)
        {
            return _hizmetDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, HizmetDelete")]
        public int DeleteSoft(int Id)
        {
            return _hizmetDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, HizmetRead, HizmetLtd")]
        public List<Hizmet> GetListPagination(PagingParams pagingParams)
        {
            return _hizmetDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _hizmetDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<Hizmet> listHizmet)
        {
            return _hizmetDal.AddListWithTransactionBySablon(listHizmet);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<Hizmet> ExcelDataProcess(DataTable dataTable)
        {
            List<Hizmet> listHizmet = new List<Hizmet>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listHizmet.Add(new Hizmet()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    BirimFiyat = row[2] != DBNull.Value ? Convert.ToDecimal(row[2].ToString()) : 0,
                    ParaBirimID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    Aciklama = row[4].ToString(),
                });
            }


            return listHizmet;
        }

    }
}