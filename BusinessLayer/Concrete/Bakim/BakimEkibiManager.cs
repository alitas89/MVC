using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class BakimEkibiManager : IBakimEkibiService
    {
        IBakimEkibiDal _bakimekibiDal;

        public BakimEkibiManager(IBakimEkibiDal bakimekibiDal)
        {
            _bakimekibiDal = bakimekibiDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, BakimEkibiRead, BakimEkibiLtd, Authorized")]
        public List<BakimEkibi> GetList()
        {
            return _bakimekibiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BakimEkibiRead, BakimEkibiLtd")]
        public BakimEkibi GetById(int Id)
        {
            return _bakimekibiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, BakimEkibiCreate")]
        public int Add(BakimEkibi bakimekibi)
        {
            return _bakimekibiDal.Add(bakimekibi);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, BakimEkibiUpdate")]
        public int Update(BakimEkibi bakimekibi)
        {
            return _bakimekibiDal.Update(bakimekibi);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimEkibiDelete")]
        public int Delete(int Id)
        {
            return _bakimekibiDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, BakimEkibiDelete")]
        public int DeleteSoft(int Id)
        {
            return _bakimekibiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, BakimEkibiRead, BakimEkibiLtd, Authorized")]
        public List<BakimEkibi> GetListPagination(PagingParams pagingParams)
        {
            return _bakimekibiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _bakimekibiDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<BakimEkibi> listBakimEkibi)
        {
            return _bakimekibiDal.AddListWithTransactionBySablon(listBakimEkibi);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<BakimEkibi> ExcelDataProcess(DataTable dataTable)
        {
            List<BakimEkibi> listBakimEkibi = new List<BakimEkibi>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listBakimEkibi.Add(new BakimEkibi()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                });
            }

            return listBakimEkibi;
        }


    }
}
