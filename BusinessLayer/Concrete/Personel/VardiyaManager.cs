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
using EntityLayer.ComplexTypes.DtoModel.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Concrete.Personel
{
    public class VardiyaManager : IVardiyaService
    {
        IVardiyaDal _vardiyaDal;

        public VardiyaManager(IVardiyaDal vardiyaDal)
        {
            _vardiyaDal = vardiyaDal;
        }

        
        [SecuredOperation(Roles = "Admin, PersonelRead, VardiyaRead, VardiyaLtd")]
        public List<Vardiya> GetList()
        {
            return _vardiyaDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, VardiyaRead, VardiyaLtd")]
        public Vardiya GetById(int Id)
        {
            return _vardiyaDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelCreate, VardiyaCreate")]
        public int Add(Vardiya vardiya)
        {
            return _vardiyaDal.Add(vardiya);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelUpdate, VardiyaUpdate")]
        public int Update(Vardiya vardiya)
        {
            return _vardiyaDal.Update(vardiya);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, VardiyaDelete")]
        public int Delete(int Id)
        {
            return _vardiyaDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, VardiyaDelete")]
        public int DeleteSoft(int Id)
        {
            return _vardiyaDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, VardiyaRead, VardiyaLtd")]
        public List<Vardiya> GetListPagination(PagingParams pagingParams)
        {
            return _vardiyaDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _vardiyaDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, VardiyaRead, VardiyaLtd")]
        public List<VardiyaDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _vardiyaDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _vardiyaDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<Vardiya> listVardiya)
        {
            return _vardiyaDal.AddListWithTransactionBySablon(listVardiya);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<Vardiya> ExcelDataProcess(DataTable dataTable)
        {
            List<Vardiya> listVardiya = new List<Vardiya>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listVardiya.Add(new Vardiya()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    BaslangicSaati = row[2].ToString(),
                    BaslangicSaati2 = row[3].ToString(),
                    BitisSaati = row[4].ToString(),
                    BitisSaati2 = row[5].ToString(),
                    SarfYeriID = row[6] != DBNull.Value ? Convert.ToInt32(row[6].ToString()) : 0,
                    BakimSuresiHesabinaDahil = row[7] != DBNull.Value ? Convert.ToBoolean(row[7].ToString()) : false,
                    DurusSuresiHesabinaDahil = row[8] != DBNull.Value ? Convert.ToBoolean(row[8].ToString()) : false,
                });
            }

            return listVardiya;
        }

    }
}
