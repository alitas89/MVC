using System.Collections.Generic;
using System.Data;
using BusinessLayer.Abstract.Varlik;
using BusinessLayer.ValidationRules.FluentValidation;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.ValidationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class MarkaManager : IMarkaService
    {
        IMarkaDal _markaDal;

        public MarkaManager(IMarkaDal markaDal)
        {
            _markaDal = markaDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, MarkaRead, MarkaLtd")]
        public List<Marka> GetList()
        {
            return _markaDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, MarkaRead, MarkaLtd")]
        public Marka GetById(int Id)
        {
            return _markaDal.Get(Id);
        }

        
        [FluentValidationAspect(typeof(MarkaValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikCreate, MarkaCreate")]
        public int Add(Marka marka)
        {
            return _markaDal.Add(marka);
        }

        
        [FluentValidationAspect(typeof(MarkaValidator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, MarkaUpdate")]
        public int Update(Marka marka)
        {
            return _markaDal.Update(marka);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, MarkaDelete")]
        public int Delete(int Id)
        {
            return _markaDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, MarkaDelete")]
        public int DeleteSoft(int Id)
        {
            return _markaDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, MarkaRead, MarkaLtd")]
        public List<Marka> GetListPagination(PagingParams pagingParams)
        {
            return _markaDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _markaDal.GetCount(filter);
        }


        public List<string> AddListWithTransactionBySablon(List<Marka> listMarka)
        {
            return _markaDal.AddListWithTransactionBySablon(listMarka);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<Marka> ExcelDataProcess(DataTable dataTable)
        {
            List<Marka> listMarka = new List<Marka>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listMarka.Add(new Marka()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                });
            }

            return listMarka;
        }
    }
}
