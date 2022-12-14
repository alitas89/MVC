using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer.Concrete.Varlik
{
    public class VarlikSablonManager : IVarlikSablonService
    {
        IVarlikSablonDal _varliksablonDal;

        public VarlikSablonManager(IVarlikSablonDal varliksablonDal)
        {
            _varliksablonDal = varliksablonDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikSablonRead, VarlikSablonLtd")]
        public List<VarlikSablon> GetList()
        {
            return _varliksablonDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikSablonRead, VarlikSablonLtd")]
        public VarlikSablon GetById(int Id)
        {
            return _varliksablonDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikCreate, VarlikSablonCreate")]
        public int Add(VarlikSablon varliksablon)
        {
            return _varliksablonDal.IsSablonDefined(varliksablon.VarlikTuruID) ? 0 : _varliksablonDal.Add(varliksablon);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, VarlikSablonUpdate")]
        public int Update(VarlikSablon varliksablon)
        {
            return _varliksablonDal.Update(varliksablon);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikSablonDelete")]
        public int Delete(int Id)
        {
            return _varliksablonDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikSablonDelete")]
        public int DeleteSoft(int Id)
        {
            return _varliksablonDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikSablonRead, VarlikSablonLtd")]
        public List<VarlikSablon> GetListPagination(PagingParams pagingParams)
        {
            return _varliksablonDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _varliksablonDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikSablonRead, VarlikSablonLtd")]
        public List<VarlikSablonDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _varliksablonDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _varliksablonDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<VarlikSablon> listVarlikSablon)
        {
            return _varliksablonDal.AddListWithTransactionBySablon(listVarlikSablon);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<VarlikSablon> ExcelDataProcess(DataTable dataTable)
        {
            List<VarlikSablon> listVarlikSablon = new List<VarlikSablon>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listVarlikSablon.Add(new VarlikSablon()
                {
                    Ad = row[0].ToString(),
                    VarlikTuruID = row[1] .ToString() != "" ? Convert.ToInt32(row[1].ToString()) : 0,
                });
            }

            return listVarlikSablon;
        }
    }

}
