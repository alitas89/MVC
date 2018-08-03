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
    public class OlcuBirimManager : IOlcuBirimService
    {
        IOlcuBirimDal _olcubirimDal;

        public OlcuBirimManager(IOlcuBirimDal olcubirimDal)
        {
            _olcubirimDal = olcubirimDal;
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeRead, OlcuBirimRead, OlcuBirimLtd")]
        public List<OlcuBirim> GetList()
        {
            return _olcubirimDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, OlcuBirimRead, OlcuBirimLtd")]
        public OlcuBirim GetById(int Id)
        {
            return _olcubirimDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeCreate, OlcuBirimCreate")]
        public int Add(OlcuBirim olcubirim)
        {
            return _olcubirimDal.Add(olcubirim);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeUpdate, OlcuBirimUpdate")]
        public int Update(OlcuBirim olcubirim)
        {
            return _olcubirimDal.Update(olcubirim);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, OlcuBirimDelete")]
        public int Delete(int Id)
        {
            return _olcubirimDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, OlcuBirimDelete")]
        public int DeleteSoft(int Id)
        {
            return _olcubirimDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, OlcuBirimRead, OlcuBirimLtd")]
        public List<OlcuBirim> GetListPagination(PagingParams pagingParams)
        {
            return _olcubirimDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _olcubirimDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<OlcuBirim> listOlcuBirim)
        {
            return _olcubirimDal.AddListWithTransactionBySablon(listOlcuBirim);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<OlcuBirim> ExcelDataProcess(DataTable dataTable)
        {
            List<OlcuBirim> listOlcuBirim = new List<OlcuBirim>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listOlcuBirim.Add(new OlcuBirim()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                });
            }

            return listOlcuBirim;
        }

    }
}
