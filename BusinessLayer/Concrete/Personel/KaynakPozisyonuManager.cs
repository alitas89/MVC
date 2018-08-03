using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract.Personel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Concrete.Personel
{
    public class KaynakPozisyonuManager : IKaynakPozisyonuService
    {
        IKaynakPozisyonuDal _kaynakpozisyonuDal;

        public KaynakPozisyonuManager(IKaynakPozisyonuDal kaynakpozisyonuDal)
        {
            _kaynakpozisyonuDal = kaynakpozisyonuDal;
        }

        
        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakPozisyonuRead, KaynakPozisyonuLtd")]
        public List<KaynakPozisyonu> GetList()
        {
            return _kaynakpozisyonuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakPozisyonuRead, KaynakPozisyonuLtd")]
        public KaynakPozisyonu GetById(int Id)
        {
            return _kaynakpozisyonuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelCreate, KaynakPozisyonuCreate")]
        public int Add(KaynakPozisyonu kaynakpozisyonu)
        {
            return _kaynakpozisyonuDal.Add(kaynakpozisyonu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelUpdate, KaynakPozisyonuUpdate")]
        public int Update(KaynakPozisyonu kaynakpozisyonu)
        {
            return _kaynakpozisyonuDal.Update(kaynakpozisyonu);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, KaynakPozisyonuDelete")]
        public int Delete(int Id)
        {
            return _kaynakpozisyonuDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, KaynakPozisyonuDelete")]
        public int DeleteSoft(int Id)
        {
            return _kaynakpozisyonuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakPozisyonuRead, KaynakPozisyonuLtd")]
        public List<KaynakPozisyonu> GetListPagination(PagingParams pagingParams)
        {
            return _kaynakpozisyonuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _kaynakpozisyonuDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<KaynakPozisyonu> listKaynakPozisyonu)
        {
            return _kaynakpozisyonuDal.AddListWithTransactionBySablon(listKaynakPozisyonu);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<KaynakPozisyonu> ExcelDataProcess(DataTable dataTable)
        {
            List<KaynakPozisyonu> listKaynakPozisyonu = new List<KaynakPozisyonu>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listKaynakPozisyonu.Add(new KaynakPozisyonu()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    UstDuzeyPozisyonID = row[2] != DBNull.Value ? Convert.ToInt32(row[2].ToString()) : 0,
                    Aciklama = row[3].ToString(),
                    Teknisyendir = row[4] != DBNull.Value ? Convert.ToBoolean(row[4].ToString()) : false,
                });
            }


            return listKaynakPozisyonu;
        }

    }
}
