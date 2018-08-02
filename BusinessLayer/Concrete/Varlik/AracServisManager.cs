using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using System.Data;
using System;

namespace BusinessLayer.Concrete.Varlik
{
    public class AracServisManager : IAracServisService
    {
        IAracServisDal _aracServisDal;

        public AracServisManager(IAracServisDal aracservisDal)
        {
            _aracServisDal = aracservisDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, AracServisRead, AracServisLtd")]
        public List<AracServis> GetList()
        {
            return _aracServisDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, AracServisRead, AracServisLtd")]
        public AracServis GetById(int Id)
        {
            return _aracServisDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikCreate, AracServisCreate")]
        public int Add(AracServis aracservis)
        {
            return _aracServisDal.Add(aracservis);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, AracServisUpdate")]
        public int Update(AracServis aracservis)
        {
            return _aracServisDal.Update(aracservis);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, AracServisDelete")]
        public int Delete(int Id)
        {
            return _aracServisDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, AracServisDelete")]
        public int DeleteSoft(int Id)
        {
            return _aracServisDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, AracServisRead, AracServisLtd")]
        public List<AracServis> GetListPagination(PagingParams pagingParams)
        {
            return _aracServisDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _aracServisDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, AracServisRead, AracServisLtd")]
        public List<AracServisDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _aracServisDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _aracServisDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<AracServis> listAracServis)
        {
            return _aracServisDal.AddListWithTransactionBySablon(listAracServis);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<AracServis> ExcelDataProcess(DataTable dataTable)
        {
            List<AracServis> listAracServis = new List<AracServis>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listAracServis.Add(new AracServis()
                {
                    IsEmriYili = row[0] != DBNull.Value ? Convert.ToInt32(row[0].ToString()) : 0,
                    FisNo = row[1].ToString(),
                    TalepEdenID = row[2] != DBNull.Value ? Convert.ToInt32(row[2].ToString()) : 0,
                    AracID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    GorevID = row[4] != DBNull.Value ? Convert.ToInt32(row[4].ToString()) : 0,
                    TalepTarih = row[5] != DBNull.Value ? Convert.ToDateTime(row[5].ToString()) : DateTime.MaxValue,
                    TalepSaat = row[6].ToString(),
                    TeslimEtmeTarih = row[7] != DBNull.Value ? Convert.ToDateTime(row[7].ToString()) : DateTime.MaxValue,
                    TeslimEtmeSaat = row[8].ToString(),
                    TeslimAlmaTarih = row[9] != DBNull.Value ? Convert.ToDateTime(row[9].ToString()) : DateTime.MaxValue,
                    TeslimAlmaSaat = row[10].ToString(),
                    TeslimAlinanKm = row[11] != DBNull.Value ? Convert.ToDecimal(row[11].ToString()) : 0,
                    TeslimEdilenKm = row[12] != DBNull.Value ? Convert.ToDecimal(row[12].ToString()) : 0,
                    KullanilanKm = row[13] != DBNull.Value ? Convert.ToDecimal(row[13].ToString()) : 0,
                    Aciklama = row[14].ToString(),
                    TeslimEdenID = row[15] != DBNull.Value ? Convert.ToInt32(row[15].ToString()) : 0,
                    TeslimAlanID = row[16] != DBNull.Value ? Convert.ToInt32(row[16].ToString()) : 0,
                    TeslimAmbarID = row[17] != DBNull.Value ? Convert.ToInt32(row[17].ToString()) : 0,
                    BolumID = row[18] != DBNull.Value ? Convert.ToInt32(row[18].ToString()) : 0,
                    VarlikDurumID = row[19] != DBNull.Value ? Convert.ToInt32(row[19].ToString()) : 0,
                    MarkaID = row[20] != DBNull.Value ? Convert.ToInt32(row[20].ToString()) : 0,
                    ModelID = row[21] != DBNull.Value ? Convert.ToInt32(row[21].ToString()) : 0,
                    SeriNo = row[22].ToString(),
                    ArizaID = row[23] != DBNull.Value ? Convert.ToInt32(row[23].ToString()) : 0,
                    HizmetID = row[24] != DBNull.Value ? Convert.ToInt32(row[24].ToString()) : 0,
                    ServisAdres = row[25].ToString(),
                });
            }

            return listAracServis;
        }
    }
}
