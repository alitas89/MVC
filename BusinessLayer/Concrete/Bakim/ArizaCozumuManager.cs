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
    public class ArizaCozumuManager : IArizaCozumuService
    {
        IArizaCozumuDal _arizacozumuDal;

        public ArizaCozumuManager(IArizaCozumuDal arizacozumuDal)
        {
            _arizacozumuDal = arizacozumuDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, ArizaCozumuRead, ArizaCozumuLtd")]
        public List<ArizaCozumu> GetList()
        {
            return _arizacozumuDal.GetList();
        }
        [SecuredOperation(Roles = "Admin, BakimRead, ArizaCozumuRead, ArizaCozumuLtd")]
        public ArizaCozumu GetById(int Id)
        {
            return _arizacozumuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, ArizaCozumuCreate")]
        public int Add(ArizaCozumu arizacozumu)
        {
            return _arizacozumuDal.Add(arizacozumu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, ArizaCozumuUpdate")]
        public int Update(ArizaCozumu arizacozumu)
        {
            return _arizacozumuDal.Update(arizacozumu);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, ArizaCozumuDelete")]
        public int Delete(int Id)
        {
            return _arizacozumuDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, ArizaCozumuDelete")]
        public int DeleteSoft(int Id)
        {
            return _arizacozumuDal.DeleteSoft(Id);
        }
        
        [SecuredOperation(Roles = "Admin, BakimRead,Editor,ArizaCozumuRead, ArizaCozumuLtd")]
        public List<ArizaCozumu> GetListPagination(PagingParams pagingParams)
        {
            return _arizacozumuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _arizacozumuDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<ArizaCozumu> listArizaCozumu)
        {
            return _arizacozumuDal.AddListWithTransactionBySablon(listArizaCozumu);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<ArizaCozumu> ExcelDataProcess(DataTable dataTable)
        {
            List<ArizaCozumu> listArizaCozumu = new List<ArizaCozumu>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listArizaCozumu.Add(new ArizaCozumu()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    TekNoktaEgitimiOlustur = row[2] != DBNull.Value ? Convert.ToBoolean(row[2].ToString()) : false,
                    Aciklama = row[3].ToString(),
                });
            }


            return listArizaCozumu;
        }
    }
}