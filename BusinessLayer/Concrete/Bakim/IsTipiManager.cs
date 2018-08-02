using System;
using System.Collections.Generic;
using System.Data;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class IsTipiManager : IIsTipiService
    {
        IIsTipiDal _isTipiDal;

        public IsTipiManager(IIsTipiDal isTipiDal)
        {
            _isTipiDal = isTipiDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, IsTipiRead, IsTipiLtd")]
        public List<IsTipi> GetList()
        {
            return _isTipiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsTipiRead, IsTipiLtd")]
        public IsTipi GetById(int Id)
        {
            return _isTipiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, IsTipiCreate")]
        public int Add(IsTipi isTipi)
        {
            return _isTipiDal.Add(isTipi);
        }
        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, IsTipiUpdate")]
        public int Update(IsTipi isTipi)
        {
            return _isTipiDal.Update(isTipi);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, IsTipiDelete")]
        public int Delete(int Id)
        {
            return _isTipiDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, IsTipiDelete")]
        public int DeleteSoft(int Id)
        {
            return _isTipiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsTipiRead, IsTipiLtd")]
        public List<IsTipi> GetListPagination(PagingParams pagingParams)
        {
            return _isTipiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _isTipiDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsTipiRead, IsTipiLtd")]
        public List<IsTipiDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _isTipiDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _isTipiDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<IsTipi> listIsTipi)
        {
            return _isTipiDal.AddListWithTransactionBySablon(listIsTipi);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<IsTipi> ExcelDataProcess(DataTable dataTable)
        {
            List<IsTipi> listIsTipi = new List<IsTipi>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listIsTipi.Add(new IsTipi()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    BakimOncelikID = row[2] != DBNull.Value ? Convert.ToInt32(row[2].ToString()) : 0,
                    IsEmriTuruID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    Aciklama = row[4].ToString(),
                });
            }

            return listIsTipi;
        }

    }
}
