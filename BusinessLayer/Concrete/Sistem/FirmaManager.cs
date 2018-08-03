using System.Collections.Generic;
using System.Data;
using BusinessLayer.Abstract.Sistem;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Concrete.Sistem
{
    public class FirmaManager : IFirmaService
    {
        IFirmaDal _firmaDal;

        public FirmaManager(IFirmaDal firmaDal)
        {
            _firmaDal = firmaDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemRead, FirmaRead, FirmaLtd")]
        public List<Firma> GetList()
        {
            return _firmaDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SistemRead, FirmaRead, FirmaLtd")]
        public Firma GetById(int Id)
        {
            return _firmaDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemCreate, FirmaCreate")]
        public int Add(Firma firma)
        {
            return _firmaDal.Add(firma);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemUpdate, FirmaUpdate")]
        public int Update(Firma firma)
        {
            return _firmaDal.Update(firma);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemDelete, FirmaDelete")]
        public int Delete(int Id)
        {
            return _firmaDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, SistemDelete, FirmaDelete")]
        public int DeleteSoft(int Id)
        {
            return _firmaDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, FirmaRead, FirmaLtd")]
        public List<Firma> GetListPagination(PagingParams pagingParams)
        {
            return _firmaDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _firmaDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<Firma> listFirma)
        {
            return _firmaDal.AddListWithTransactionBySablon(listFirma);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<Firma> ExcelDataProcess(DataTable dataTable)
        {
            List<Firma> listFirma = new List<Firma>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listFirma.Add(new Firma()
                {
                    Ad = row[0].ToString(),
                    Kod = row[1].ToString(),
                    Sorumlu = row[2].ToString(),
                    Adres = row[3].ToString(),
                    Telefon = row[4].ToString(),
                });
            }

            return listFirma;
        }

    }

}