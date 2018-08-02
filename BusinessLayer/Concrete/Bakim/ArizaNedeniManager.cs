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
    public class ArizaNedeniManager : IArizaNedeniService
    {
        IArizaNedeniDal _arizanedeniDal;

        public ArizaNedeniManager(IArizaNedeniDal arizanedeniDal)
        {
            _arizanedeniDal = arizanedeniDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, ArizaNedeniRead, ArizaNedeniLtd")]
        public List<ArizaNedeni> GetList()
        {
            return _arizanedeniDal.GetList();
        }
        [SecuredOperation(Roles = "Admin, BakimRead, ArizaNedeniRead, ArizaNedeniLtd")]
        public ArizaNedeni GetById(int Id)
        {
            return _arizanedeniDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, ArizaNedeniCreate")]
        public int Add(ArizaNedeni arizanedeni)
        {
            return _arizanedeniDal.Add(arizanedeni);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, ArizaNedeniUpdate")]
        public int Update(ArizaNedeni arizanedeni)
        {
            return _arizanedeniDal.Update(arizanedeni);
        }
        
        [SecuredOperation(Roles = "Admin, BakimDelete, ArizaNedeniDelete")]
        public int Delete(int Id)
        {
            return _arizanedeniDal.Delete(Id);
        }
        
        [SecuredOperation(Roles = "Admin, BakimDelete, ArizaNedeniDelete")]
        public int DeleteSoft(int Id)
        {
            return _arizanedeniDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, ArizaNedeniRead, ArizaNedeniLtd")]
        public List<ArizaNedeni> GetListPagination(PagingParams pagingParams)
        {
            return _arizanedeniDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _arizanedeniDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<ArizaNedeni> listArizaNedeni)
        {
            return _arizanedeniDal.AddListWithTransactionBySablon(listArizaNedeni);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<ArizaNedeni> ExcelDataProcess(DataTable dataTable)
        {
            List<ArizaNedeni> listArizaNedeni = new List<ArizaNedeni>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listArizaNedeni.Add(new ArizaNedeni()
                {
                    Kod = row[0].ToString(),
                    GenelKod = row[1] != DBNull.Value ? Convert.ToBoolean(row[1].ToString()) : false,
                    Ad = row[2].ToString(),
                    UretimiDurdurur = row[3] != DBNull.Value ? Convert.ToBoolean(row[3].ToString()) : false,
                    NedenAnaliziZorunluOlmali = row[4] != DBNull.Value ? Convert.ToInt32(row[4].ToString()) : 0,
                    Aciklama = row[5].ToString(),
                });
            }

            return listArizaNedeni;
        }
    }
}