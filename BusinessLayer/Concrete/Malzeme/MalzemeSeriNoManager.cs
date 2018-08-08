using System;
using System.Collections.Generic;
using System.Data;
using BusinessLayer.Abstract.Malzeme;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.DtoModel.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Concrete.Malzeme
{
    public class MalzemeSeriNoManager : IMalzemeSeriNoService
    {
        IMalzemeSeriNoDal _malzemeserinoDal;

        public MalzemeSeriNoManager(IMalzemeSeriNoDal malzemeserinoDal)
        {
            _malzemeserinoDal = malzemeserinoDal;
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeSeriNoRead, MalzemeSeriNoLtd")]
        public List<MalzemeSeriNo> GetList()
        {
            return _malzemeserinoDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeSeriNoRead, MalzemeSeriNoLtd")]
        public MalzemeSeriNo GetById(int Id)
        {
            return _malzemeserinoDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeCreate, MalzemeSeriNoCreate")]
        public int Add(MalzemeSeriNo malzemeserino)
        {
            return _malzemeserinoDal.Add(malzemeserino);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeUpdate, MalzemeSeriNoUpdate")]
        public int Update(MalzemeSeriNo malzemeserino)
        {
            return _malzemeserinoDal.Update(malzemeserino);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemeSeriNoDelete")]
        public int Delete(int Id)
        {
            return _malzemeserinoDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemeSeriNoDelete")]
        public int DeleteSoft(int Id)
        {
            return _malzemeserinoDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeSeriNoRead, MalzemeSeriNoLtd")]
        public List<MalzemeSeriNo> GetListPagination(PagingParams pagingParams)
        {
            return _malzemeserinoDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _malzemeserinoDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeSeriNoRead, MalzemelerSeriNoLtd")]
        public List<MalzemeSeriNoDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _malzemeserinoDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _malzemeserinoDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<MalzemeSeriNo> listMalzemeSeriNo)
        {
            return _malzemeserinoDal.AddListWithTransactionBySablon(listMalzemeSeriNo);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<MalzemeSeriNo> ExcelDataProcess(DataTable dataTable)
        {
            List<MalzemeSeriNo> listMalzemeSeriNo = new List<MalzemeSeriNo>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listMalzemeSeriNo.Add(new MalzemeSeriNo()
                {
                    SeriNo = row[0].ToString(),
                    OzelKod = row[1].ToString(),
                    MalzemeID = row[2] .ToString() != "" ? Convert.ToInt32(row[2].ToString()) : 0,
                    Aciklama = row[3].ToString(),
                });
            }


            return listMalzemeSeriNo;
        }

    }
}
