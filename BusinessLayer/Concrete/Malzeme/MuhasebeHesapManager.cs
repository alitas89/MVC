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
    public class MuhasebeHesapManager : IMuhasebeHesapService
    {
        IMuhasebeHesapDal _muhasebehesapDal;

        public MuhasebeHesapManager(IMuhasebeHesapDal muhasebehesapDal)
        {
            _muhasebehesapDal = muhasebehesapDal;
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeRead, MuhasebeHesapRead, MuhasebeHesapLtd")]
        public List<MuhasebeHesap> GetList()
        {
            return _muhasebehesapDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MuhasebeHesapRead, MuhasebeHesapLtd")]
        public MuhasebeHesap GetById(int Id)
        {
            return _muhasebehesapDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeCreate, MuhasebeHesapCreate")]
        public int Add(MuhasebeHesap muhasebehesap)
        {
            return _muhasebehesapDal.Add(muhasebehesap);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, MalzemeUpdate, MuhasebeHesapUpdate")]
        public int Update(MuhasebeHesap muhasebehesap)
        {
            return _muhasebehesapDal.Update(muhasebehesap);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MuhasebeHesapDelete")]
        public int Delete(int Id)
        {
            return _muhasebehesapDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, MalzemeDelete, MuhasebeHesapDelete")]
        public int DeleteSoft(int Id)
        {
            return _muhasebehesapDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, MalzemeRead, MuhasebeHesapRead, MuhasebeHesapLtd")]
        public List<MuhasebeHesap> GetListPagination(PagingParams pagingParams)
        {
            return _muhasebehesapDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _muhasebehesapDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<MuhasebeHesap> listMuhasebeHesap)
        {
            return _muhasebehesapDal.AddListWithTransactionBySablon(listMuhasebeHesap);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<MuhasebeHesap> ExcelDataProcess(DataTable dataTable)
        {
            List<MuhasebeHesap> listMuhasebeHesap = new List<MuhasebeHesap>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listMuhasebeHesap.Add(new MuhasebeHesap()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                });
            }

            return listMuhasebeHesap;
        }

    }
}
