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
    public class BeklemeIptalNedeniManager : IBeklemeIptalNedeniService
    {
        IBeklemeIptalNedeniDal _beklemeıptalnedeniDal;

        public BeklemeIptalNedeniManager(IBeklemeIptalNedeniDal beklemeıptalnedeniDal)
        {
            _beklemeıptalnedeniDal = beklemeıptalnedeniDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, BeklemeIptalNedeniRead, BeklemeIptalNedeniLtd")]
        public List<BeklemeIptalNedeni> GetList()
        {
            return _beklemeıptalnedeniDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BeklemeIptalNedeniRead, BeklemeIptalNedeniLtd")]
        public BeklemeIptalNedeni GetById(int Id)
        {
            return _beklemeıptalnedeniDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, BeklemeIptalNedeniCreate")]
        public int Add(BeklemeIptalNedeni beklemeıptalnedeni)
        {
            return _beklemeıptalnedeniDal.Add(beklemeıptalnedeni);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, BeklemeIptalNedeniUpdate")]
        public int Update(BeklemeIptalNedeni beklemeıptalnedeni)
        {
            return _beklemeıptalnedeniDal.Update(beklemeıptalnedeni);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BeklemeIptalNedeniDelete")]
        public int Delete(int Id)
        {
            return _beklemeıptalnedeniDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BeklemeIptalNedeniDelete")]
        public int DeleteSoft(int Id)
        {
            return _beklemeıptalnedeniDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BeklemeIptalNedeniRead, BeklemeIptalNedeniLtd")]
        public List<BeklemeIptalNedeni> GetListPagination(PagingParams pagingParams)
        {
            return _beklemeıptalnedeniDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _beklemeıptalnedeniDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<BeklemeIptalNedeni> listBeklemeIptalNedeni)
        {
            return _beklemeıptalnedeniDal.AddListWithTransactionBySablon(listBeklemeIptalNedeni);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<BeklemeIptalNedeni> ExcelDataProcess(DataTable dataTable)
        {
            List<BeklemeIptalNedeni> listBeklemeIptalNedeni = new List<BeklemeIptalNedeni>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listBeklemeIptalNedeni.Add(new BeklemeIptalNedeni()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                    IsEmriniKapsayanPeriyodikBakimOlustur = row[3] != DBNull.Value ? Convert.ToBoolean(row[3].ToString()) : false,
                    IptalEdilenOtonomBakimdanIsEmriOlustur = row[4] != DBNull.Value ? Convert.ToBoolean(row[4].ToString()) : false,
                });
            }



            return listBeklemeIptalNedeni;
        }
    }
}