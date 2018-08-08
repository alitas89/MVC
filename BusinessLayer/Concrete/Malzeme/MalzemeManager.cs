using BusinessLayer.Abstract.Malzeme;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using System.Data;
using System;

namespace BusinessLayer.Concrete.Malzeme
{
    public class MalzemeManager : IMalzemeService
    {
        IMalzemeDal _malzemeDal;

        public MalzemeManager(IMalzemeDal malzemeDal)
        {
            _malzemeDal = malzemeDal;
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemelerRead, MalzemelerLtd")]
        public List<EntityLayer.Concrete.Malzeme.Malzeme> GetList()
        {
            return _malzemeDal.GetList();
        }
        
        [SecuredOperation(Roles = "Admin, MalzemeRead,MalzemelerRead, MalzemelerLtd")]
        public List<MalzemeAmbarDetay> GetMalzemeAmbarDetay(int MalzemeID)
        {
            return _malzemeDal.GetMalzemeAmbarDetay(MalzemeID);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemelerRead, MalzemelerLtd")]
        public EntityLayer.Concrete.Malzeme.Malzeme GetById(int Id)
        {
            return _malzemeDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeCreate, MalzemelerCreate")]
        public int Add(EntityLayer.Concrete.Malzeme.Malzeme malzeme)
        {
            return _malzemeDal.IsKodDefined(malzeme.Kod) ? 0 : _malzemeDal.Add(malzeme);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeUpdate, MalzemelerUpdate")]
        public int Update(EntityLayer.Concrete.Malzeme.Malzeme malzeme)
        {
            return _malzemeDal.Update(malzeme);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemelerDelete")]
        public int Delete(int Id)
        {
            return _malzemeDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemelerDelete")]
        public int DeleteSoft(int Id)
        {
            return _malzemeDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemelerRead, MalzemelerLtd")]
        public List<EntityLayer.Concrete.Malzeme.Malzeme> GetListPagination(PagingParams pagingParams)
        {
            return _malzemeDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _malzemeDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemelerRead, MalzemelerLtd")]
        public List<MalzemeDto> GetListDto()
        {
            return _malzemeDal.GetListDto();
        }
        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemelerRead, MalzemelerLtd")]
        public List<MalzemeDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _malzemeDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _malzemeDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<EntityLayer.Concrete.Malzeme.Malzeme> listMalzeme)
        {
            return _malzemeDal.AddListWithTransactionBySablon(listMalzeme);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<EntityLayer.Concrete.Malzeme.Malzeme> ExcelDataProcess(DataTable dataTable)
        {
            List<EntityLayer.Concrete.Malzeme.Malzeme> listMalzeme = new List<EntityLayer.Concrete.Malzeme.Malzeme>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listMalzeme.Add(new EntityLayer.Concrete.Malzeme.Malzeme()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    OlcuBirimID = row[2] .ToString() != "" ? Convert.ToInt32(row[2].ToString()) : 0,
                    MalzemeGrupID = row[3].ToString(),
                    MalzemeAltGrupID = row[4].ToString(),
                    SeriNo = row[5].ToString(),
                    MarkaID = row[6] .ToString() != "" ? Convert.ToInt32(row[6].ToString()) : 0,
                    ModelID = row[7] .ToString() != "" ? Convert.ToInt32(row[7].ToString()) : 0,
                });
            }

            return listMalzeme;
        }
    }
}
