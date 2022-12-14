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
    public class KaynakSinifiManager : IKaynakSinifiService
    {
        IKaynakSinifiDal _kaynaksinifiDal;

        public KaynakSinifiManager(IKaynakSinifiDal kaynaksinifiDal)
        {
            _kaynaksinifiDal = kaynaksinifiDal;
        }

        
        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakSinifiRead, KaynakSinifiLtd")]
        public List<KaynakSinifi> GetList()
        {
            return _kaynaksinifiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakSinifiRead, KaynakSinifiLtd")]
        public KaynakSinifi GetById(int Id)
        {
            return _kaynaksinifiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelCreate, KaynakSinifiCreate")]
        public int Add(KaynakSinifi kaynaksinifi)
        {
            return _kaynaksinifiDal.Add(kaynaksinifi);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelUpdate, KaynakSinifiUpdate")]
        public int Update(KaynakSinifi kaynaksinifi)
        {
            return _kaynaksinifiDal.Update(kaynaksinifi);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, KaynakSinifiDelete")]
        public int Delete(int Id)
        {
            return _kaynaksinifiDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, KaynakSinifiDelete")]
        public int DeleteSoft(int Id)
        {
            return _kaynaksinifiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakSinifiRead, KaynakSinifiLtd")]
        public List<KaynakSinifi> GetListPagination(PagingParams pagingParams)
        {
            return _kaynaksinifiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _kaynaksinifiDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<KaynakSinifi> listKaynakSinifi)
        {
            return _kaynaksinifiDal.AddListWithTransactionBySablon(listKaynakSinifi);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<KaynakSinifi> ExcelDataProcess(DataTable dataTable)
        {
            List<KaynakSinifi> listKaynakSinifi = new List<KaynakSinifi>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listKaynakSinifi.Add(new KaynakSinifi()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                });
            }

            return listKaynakSinifi;
        }

    }
}
