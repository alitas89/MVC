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
using EntityLayer.ComplexTypes.DtoModel.Others;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class KisimManager : IKisimService
    {
        IKisimDal _kisimDal;

        public KisimManager(IKisimDal kisimDal)
        {
            _kisimDal = kisimDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, KisimRead, KisimLtd")]
        public List<Kisim> GetList()
        {
            return _kisimDal.GetList();
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, KisimRead, KisimLtd")]
        public List<Kisim> GetList(int SarfYeriID)
        {
            return _kisimDal.GetList(SarfYeriID);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, KisimRead, KisimLtd")]
        public List<KisimDto> GetListDto()
        {
            return _kisimDal.GetListDto();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, KisimRead, KisimLtd")]
        public Kisim GetById(int Id)
        {
            return _kisimDal.Get(Id);
        }

        
        [FluentValidationAspect(typeof(KisimValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikCreate, KisimCreate")]
        public int Add(Kisim kisim)
        {            
            //Kod Kontrolü - Aynı koda sahip kayıt varsa ekleme yapılamaz!
            return _kisimDal.IsKodDefined(kisim.Kod) ? 0 : _kisimDal.Add(kisim);
        }

        
        [FluentValidationAspect(typeof(KisimValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, KisimUpdate")]
        public int Update(Kisim kisim)
        {
            //Kod Kontrolü - Aynı koda sahip kayıt varsa güncelleme yapılamaz! (Kendisi dışındaki bir kod olmalı)
            if (_kisimDal.IsKodDefined(kisim.Kod))
            {
                //Var olan kod kendi kodu mu?
                return _kisimDal.Get(kisim.KisimID).Kod == kisim.Kod ? _kisimDal.Update(kisim) : 0;
            }
            else
            {
                return _kisimDal.Update(kisim);
            }
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, KisimDelete")]
        public int Delete(int Id)
        {
            return _kisimDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, KisimDelete")]
        public int DeleteSoft(int Id)
        {
            return _kisimDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, KisimRead, KisimLtd")]
        public List<Kisim> GetListPagination(PagingParams pagingParams)
        {
            return _kisimDal.GetListPagination(pagingParams);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, KisimRead, KisimLtd")]
        public List<KisimDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _kisimDal.GetListPaginationDto(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _kisimDal.GetCount(filter);
        }

        public int GetCountDto(string filter = "")
        {
            return _kisimDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<Kisim> listKisim)
        {
            return _kisimDal.AddListWithTransactionBySablon(listKisim);
        }

        public List<ColumnNameTemp> GetColumnNames(string tableName)
        {
            return _kisimDal.GetColumnNames(tableName);
        }

        public List<Kisim> ExcelDataProcess(DataTable dataTable)
        {
            List<Kisim> listKisim = new List<Kisim>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listKisim.Add(new Kisim()
                {
                    Kod = row[0] + "",
                    Ad = row[1] + "",
                    Butce = row[2] .ToString() != "" ? Convert.ToDecimal(row[2] + "") : 0,
                    HedeflenenButce = row[3] .ToString() != "" ? Convert.ToDecimal(row[3] + "") : 0,
                    VardiyaSinifID = row[4] .ToString() != "" ? Convert.ToInt32(row[4] + "") : 0,
                    SarfYeriID = row[5] .ToString() != "" ? Convert.ToInt32(row[5] + "") : 0,
                    Aciklama = row[6] + ""
                });
            }

            return listKisim;
        }
    }
}
