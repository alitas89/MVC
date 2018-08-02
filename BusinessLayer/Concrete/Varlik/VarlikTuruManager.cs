using System.Collections.Generic;
using System.Data;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class VarlikTuruManager : IVarlikTuruService
    {
        IVarlikTuruDal _varlikturuDal;

        public VarlikTuruManager(IVarlikTuruDal varlikturuDal)
        {
            _varlikturuDal = varlikturuDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikTuruRead, VarlikTuruLtd")]
        public List<VarlikTuru> GetList()
        {
            return _varlikturuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikTuruRead, VarlikTuruLtd")]
        public VarlikTuru GetById(int Id)
        {
            return _varlikturuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikCreate, VarlikTuruCreate")]
        public int Add(VarlikTuru varlikturu)
        {
            return _varlikturuDal.Add(varlikturu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, VarlikTuruUpdate")]
        public int Update(VarlikTuru varlikturu)
        {
            return _varlikturuDal.Update(varlikturu);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikTuruDelete")]
        public int Delete(int Id)
        {
            return _varlikturuDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarlikTuruDelete")]
        public int DeleteSoft(int Id)
        {
            return _varlikturuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarlikTuruRead, VarlikTuruLtd")]
        public List<VarlikTuru> GetListPagination(PagingParams pagingParams)
        {
            return _varlikturuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _varlikturuDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<VarlikTuru> listVarlikTuru)
        {
            return _varlikturuDal.AddListWithTransactionBySablon(listVarlikTuru);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<VarlikTuru> ExcelDataProcess(DataTable dataTable)
        {
            List<VarlikTuru> listVarlikTuru = new List<VarlikTuru>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listVarlikTuru.Add(new VarlikTuru()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                });
            }

            return listVarlikTuru;
        }
    }
}