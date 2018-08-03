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
    public class VardiyaSinifiManager : IVardiyaSinifiService
    {
        IVardiyaSinifiDal _vardiyasinifiDal;

        public VardiyaSinifiManager(IVardiyaSinifiDal vardiyasinifiDal)
        {
            _vardiyasinifiDal = vardiyasinifiDal;
        }

        
        [SecuredOperation(Roles = "Admin, PersonelRead, VardiyaSinifiRead, VardiyaSinifiLtd")]
        public List<VardiyaSinifi> GetList()
        {
            return _vardiyasinifiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, VardiyaSinifiRead, VardiyaSinifiLtd")]
        public VardiyaSinifi GetById(int Id)
        {
            return _vardiyasinifiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelCreate, VardiyaSinifiCreate")]
        public int Add(VardiyaSinifi vardiyasinifi)
        {
            return _vardiyasinifiDal.Add(vardiyasinifi);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelUpdate, VardiyaSinifiUpdate")]
        public int Update(VardiyaSinifi vardiyasinifi)
        {
            return _vardiyasinifiDal.Update(vardiyasinifi);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, VardiyaSinifiDelete")]
        public int Delete(int Id)
        {
            return _vardiyasinifiDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, VardiyaSinifiDelete")]
        public int DeleteSoft(int Id)
        {
            return _vardiyasinifiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, VardiyaSinifiRead, VardiyaSinifiLtd")]
        public List<VardiyaSinifi> GetListPagination(PagingParams pagingParams)
        {
            return _vardiyasinifiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _vardiyasinifiDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<VardiyaSinifi> listVardiyaSinifi)
        {
            return _vardiyasinifiDal.AddListWithTransactionBySablon(listVardiyaSinifi);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<VardiyaSinifi> ExcelDataProcess(DataTable dataTable)
        {
            List<VardiyaSinifi> listVardiyaSinifi = new List<VardiyaSinifi>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listVardiyaSinifi.Add(new VardiyaSinifi()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                });
            }

            return listVardiyaSinifi;
        }

    }
}
