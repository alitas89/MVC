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
    public class VarlikGrupManager : IVarlikGrupService
    {
        IVarlikGrupDal _varlikgrupDal;

        public VarlikGrupManager(IVarlikGrupDal varlikgrupDal)
        {
            _varlikgrupDal = varlikgrupDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikGrupRead, VarlikGrupLtd")]
        public List<VarlikGrup> GetList()
        {
            return _varlikgrupDal.GetList();
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikGrupRead, VarlikGrupLtd")]
        public List<VarlikGrupDto> GetListDto()
        {
            return _varlikgrupDal.GetListDto();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikGrupRead, VarlikGrupLtd")]
        public VarlikGrup GetById(int Id)
        {
            return _varlikgrupDal.Get(Id);
        }

        
        [FluentValidationAspect(typeof(VarlikGrupValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikCreate, VarlikGrupCreate")]
        public int Add(VarlikGrup varlikgrup)
        {
            return _varlikgrupDal.Add(varlikgrup);
        }

        
        [FluentValidationAspect(typeof(VarlikGrupValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, VarlikGrupUpdate")]
        public int Update(VarlikGrup varlikgrup)
        {
            return _varlikgrupDal.Update(varlikgrup);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikGrupDelete")]
        public int Delete(int Id)
        {
            return _varlikgrupDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikGrupDelete")]
        public int DeleteSoft(int Id)
        {
            return _varlikgrupDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikGrupRead, VarlikGrupLtd")]
        public List<VarlikGrup> GetListPagination(PagingParams pagingParams)
        {
            return _varlikgrupDal.GetListPagination(pagingParams);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikGrupRead, VarlikGrupLtd")]
        public List<VarlikGrupDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _varlikgrupDal.GetListPaginationDto(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _varlikgrupDal.GetCount(filter);
        }

        public int GetCountDto(string filter = "")
        {
            return _varlikgrupDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<VarlikGrup> listVarlikGrup)
        {
            return _varlikgrupDal.AddListWithTransactionBySablon(listVarlikGrup);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<VarlikGrup> ExcelDataProcess(DataTable dataTable)
        {
            List<VarlikGrup> listVarlikGrup = new List<VarlikGrup>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listVarlikGrup.Add(new VarlikGrup()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    IsTipiID = row[2] .ToString() != "" ? Convert.ToInt32(row[2].ToString()) : 0,
                    Aciklama1 = row[3].ToString(),
                    Aciklama2 = row[4].ToString(),
                    Aciklama3 = row[5].ToString(),
                });
            }

            return listVarlikGrup;
        }

    }
}
