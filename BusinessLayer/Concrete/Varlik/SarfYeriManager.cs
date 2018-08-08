using System;
using System.Collections.Generic;
using System.Data;
using BusinessLayer.Abstract.Varlik;
using BusinessLayer.ValidationRules.FluentValidation;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.ValidationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class SarfYeriManager : ISarfYeriService
    {
        ISarfYeriDal _sarfyeriDal;

        public SarfYeriManager(ISarfYeriDal sarfyeriDal)
        {
            _sarfyeriDal = sarfyeriDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, SarfYeriRead, SarfYeriLtd")]
        public List<SarfYeri> GetList()
        {
            return _sarfyeriDal.GetList();
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, SarfYeriRead, SarfYeriLtd")]
        public List<SarfYeri> GetList(int IsletmeID)
        {
            return _sarfyeriDal.GetList(IsletmeID);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, SarfYeriRead, SarfYeriLtd")]
        public List<SarfYeriDto> GetListDto()
        {
            return _sarfyeriDal.GetListDto();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, SarfYeriRead, SarfYeriLtd")]
        public SarfYeri GetById(int Id)
        {
            return _sarfyeriDal.Get(Id);
        }

        
        [FluentValidationAspect(typeof(SarfYeriValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikCreate, SarfYeriCreate")]
        public int Add(SarfYeri sarfyeri)
        {
            //Kod Kontrolü - Aynı koda sahip kayıt varsa ekleme yapılamaz!
            return _sarfyeriDal.IsKodDefined(sarfyeri.Kod) ? 0 : _sarfyeriDal.Add(sarfyeri);
        }

        
        [FluentValidationAspect(typeof(SarfYeriValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, SarfYeriUpdate")]
        public int Update(SarfYeri sarfyeri)
        {    
            //Kod Kontrolü - Aynı koda sahip kayıt varsa güncelleme yapılamaz! (Kendisi dışındaki bir kod olmalı)
            if (_sarfyeriDal.IsKodDefined(sarfyeri.Kod))
            {
                //Var olan kod kendi kodu mu?
                return _sarfyeriDal.Get(sarfyeri.SarfYeriID).Kod == sarfyeri.Kod ? _sarfyeriDal.Update(sarfyeri) : 0;
            }
            else
            {
                return _sarfyeriDal.Update(sarfyeri);
            }
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, SarfYeriDelete")]
        public int Delete(int Id)
        {
            return _sarfyeriDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, SarfYeriDelete")]
        public int DeleteSoft(int Id)
        {
            return _sarfyeriDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, SarfYeriRead, SarfYeriLtd")]
        public List<SarfYeri> GetListPagination(PagingParams pagingParams)
        {
            return _sarfyeriDal.GetListPagination(pagingParams);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, SarfYeriRead, SarfYeriLtd")]
        public List<SarfYeriDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _sarfyeriDal.GetListPaginationDto(pagingParams);
        }

        public List<string> AddListWithTransactionBySablon(List<SarfYeri> listSarfYeri)
        {
            return _sarfyeriDal.AddListWithTransactionBySablon(listSarfYeri);
        }

        public int GetCount(string filter = "")
        {
            return _sarfyeriDal.GetCount(filter);
        }

        public int GetCountDto(string filter = "")
        {
            return _sarfyeriDal.GetCountDto(filter);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<SarfYeri> ExcelDataProcess(DataTable dataTable)
        {
            List<SarfYeri> listSarfYeri = new List<SarfYeri>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listSarfYeri.Add(new SarfYeri()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Butce = row[2] .ToString() != "" ? Convert.ToDecimal(row[2].ToString()) : 0,
                    HedeflenenButce = row[3] .ToString() != "" ? Convert.ToDecimal(row[3].ToString()) : 0,
                    VardiyaSinifID = row[4] .ToString() != "" ? Convert.ToInt32(row[4].ToString()) : 0,
                    IsletmeID = row[5] .ToString() != "" ? Convert.ToInt32(row[5].ToString()) : 0,
                    Telefon1 = row[6].ToString(),
                    Telefon2 = row[7].ToString(),
                    FaxNo = row[8].ToString(),
                    Email = row[9].ToString(),
                    WebUrl = row[10].ToString(),
                    LogoDosyaYolu = row[11].ToString(),
                    Aciklama = row[12].ToString(),
                });
            }

            return listSarfYeri;
        }
    }
}
