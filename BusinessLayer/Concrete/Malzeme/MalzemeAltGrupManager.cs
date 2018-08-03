using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Malzeme;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Malzeme;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Concrete.Malzeme
{
    public class MalzemeAltGrupManager : IMalzemeAltGrupService
    {
        IMalzemeAltGrupDal _malzemealtgrupDal;

        public MalzemeAltGrupManager(IMalzemeAltGrupDal malzemealtgrupDal)
        {
            _malzemealtgrupDal = malzemealtgrupDal;
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeAltGrupRead, MalzemeAltGrupLtd")]
        public List<MalzemeAltGrup> GetList()
        {
            return _malzemealtgrupDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeAltGrupRead, MalzemeAltGrupLtd")]
        public MalzemeAltGrup GetById(int Id)
        {
            return _malzemealtgrupDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeCreate, MalzemeAltGrupCreate")]
        public int Add(MalzemeAltGrup malzemealtgrup)
        {
            return _malzemealtgrupDal.Add(malzemealtgrup);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeUpdate, MalzemeAltGrupUpdate")]
        public int Update(MalzemeAltGrup malzemealtgrup)
        {
            return _malzemealtgrupDal.Update(malzemealtgrup);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemeAltGrupDelete")]
        public int Delete(int Id)
        {
            return _malzemealtgrupDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MalzemeAltGrupDelete")]
        public int DeleteSoft(int Id)
        {
            return _malzemealtgrupDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MalzemeAltGrupRead, MalzemeAltGrupLtd")]
        public List<MalzemeAltGrup> GetListPagination(PagingParams pagingParams)
        {
            return _malzemealtgrupDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _malzemealtgrupDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<MalzemeAltGrup> listMalzemeAltGrup)
        {
            return _malzemealtgrupDal.AddListWithTransactionBySablon(listMalzemeAltGrup);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<MalzemeAltGrup> ExcelDataProcess(DataTable dataTable)
        {
            List<MalzemeAltGrup> listMalzemeAltGrup = new List<MalzemeAltGrup>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listMalzemeAltGrup.Add(new MalzemeAltGrup()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                });
            }

            return listMalzemeAltGrup;
        }

    }
}
