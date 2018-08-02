using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using System.Data;

namespace BusinessLayer.Concrete.Varlik
{
    public class AkaryakitAlimFisManager : IAkaryakitAlimFisService
    {
        IAkaryakitAlimFisDal _akaryakitalimfisDal;

        public AkaryakitAlimFisManager(IAkaryakitAlimFisDal akaryakitalimfisDal)
        {
            _akaryakitalimfisDal = akaryakitalimfisDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, AkaryakitAlimFisRead, AkaryakitAlimFisLtd")]
        public List<AkaryakitAlimFis> GetList()
        {
            return _akaryakitalimfisDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, AkaryakitAlimFisRead, AkaryakitAlimFisLtd")]
        public AkaryakitAlimFis GetById(int Id)
        {
            return _akaryakitalimfisDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikCreate, AkaryakitAlimFisCreate")]
        public int Add(AkaryakitAlimFis akaryakitalimfis)
        {
            return _akaryakitalimfisDal.Add(akaryakitalimfis);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, AkaryakitAlimFisUpdate")]
        public int Update(AkaryakitAlimFis akaryakitalimfis)
        {
            return _akaryakitalimfisDal.Update(akaryakitalimfis);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, AkaryakitAlimFisDelete")]
        public int Delete(int Id)
        {
            return _akaryakitalimfisDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, AkaryakitAlimFisDelete")]
        public int DeleteSoft(int Id)
        {
            return _akaryakitalimfisDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, AkaryakitAlimFisRead, AkaryakitAlimFisLtd")]
        public List<AkaryakitAlimFis> GetListPagination(PagingParams pagingParams)
        {
            return _akaryakitalimfisDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _akaryakitalimfisDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, AkaryakitAlimFisRead, AkaryakitAlimFisLtd")]
        public List<AkaryakitAlimFisDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _akaryakitalimfisDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _akaryakitalimfisDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<AkaryakitAlimFis> listAkaryakitAlimFis)
        {
            return _akaryakitalimfisDal.AddListWithTransactionBySablon(listAkaryakitAlimFis);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<AkaryakitAlimFis> ExcelDataProcess(DataTable dataTable)
        {
            List<AkaryakitAlimFis> listAkaryakitAlimFis = new List<AkaryakitAlimFis>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listAkaryakitAlimFis.Add(new AkaryakitAlimFis()
                {
                    FisNo = row[0].ToString(),
                    AracID = row[1] != DBNull.Value ? Convert.ToInt32(row[1].ToString()) : 0,
                    YakitID = row[2] != DBNull.Value ? Convert.ToInt32(row[2].ToString()) : 0,
                    AmbarID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    Miktar = row[4] != DBNull.Value ? Convert.ToDecimal(row[4].ToString()) : 0,
                    BirimFiyat = row[5] != DBNull.Value ? Convert.ToDecimal(row[5].ToString()) : 0,
                    Iskonto = row[6] != DBNull.Value ? Convert.ToDecimal(row[6].ToString()) : 0,
                    ToplamAkaryakitTutari = row[7] != DBNull.Value ? Convert.ToDecimal(row[7].ToString()) : 0,
                    MasrafYeriID = row[8] != DBNull.Value ? Convert.ToInt32(row[8].ToString()) : 0,
                    YakitAlanKisiID = row[9] != DBNull.Value ? Convert.ToInt32(row[9].ToString()) : 0,
                    SaticiID = row[10] != DBNull.Value ? Convert.ToInt32(row[10].ToString()) : 0,
                    YakitAlimTarih = row[11] != DBNull.Value ? Convert.ToDateTime(row[11].ToString()) : DateTime.MaxValue,
                    YakitAlimSaat = row[12].ToString(),
                    AracKm = row[13] != DBNull.Value ? Convert.ToDecimal(row[13].ToString()) : 0,
                    Aciklama = row[14].ToString(),
                });
            }

            return listAkaryakitAlimFis;
        }

    }
}
