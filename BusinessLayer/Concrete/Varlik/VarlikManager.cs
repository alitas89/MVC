using System;
using System.Collections.Generic;
using System.Data;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class VarlikManager : IVarlikService
    {
        IVarlikDal _varlikDal;

        public VarlikManager(IVarlikDal varlikDal)
        {
            _varlikDal = varlikDal;
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, VarliklarRead, VarliklarLtd")]
        public List<EntityLayer.Concrete.Varlik.Varlik> GetList()
        {
            return _varlikDal.GetList();
        }

        
        [SecuredOperation(Roles = "Admin, VarlikRead, VarliklarRead, VarliklarLtd")]
        public List<EntityLayer.Concrete.Varlik.Varlik> GetListByKisimID(int KisimID)
        {
            return _varlikDal.GetListByKisimID(KisimID);
        }
        
        [SecuredOperation(Roles = "Admin, VarlikRead, VarliklarRead, VarliklarLtd")]
        public List<EntityLayer.Concrete.Varlik.Varlik> GetListByKaynakID(int KaynakID)
        {
            return _varlikDal.GetListByKaynakID(KaynakID);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarliklarRead, VarliklarLtd")]
        public EntityLayer.Concrete.Varlik.Varlik GetById(int Id)
        {
            return _varlikDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikCreate, VarliklarCreate")]
        public int Add(EntityLayer.Concrete.Varlik.Varlik varlik)
        {
            //Kod Kontrolü - Aynı koda sahip kayıt varsa ekleme yapılamaz!
            return _varlikDal.IsKodDefined(varlik.Kod) ? 0 : _varlikDal.Add(varlik);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, VarlikUpdate, VarliklarUpdate")]
        public int Update(EntityLayer.Concrete.Varlik.Varlik varlik)
        {
            //Kod Kontrolü - Aynı koda sahip kayıt varsa güncelleme yapılamaz! (Kendisi dışındaki bir kod olmalı)
            if (_varlikDal.IsKodDefined(varlik.Kod))
            {
                //Var olan kod kendi kodu mu?
                return _varlikDal.Get(varlik.VarlikID).Kod == varlik.Kod ? _varlikDal.Update(varlik) : 0;
            }
            else
            {
                return _varlikDal.Update(varlik);
            }
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete, VarliklarDelete")]
        public int Delete(int Id)
        {
            return _varlikDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, VarlikDelete VarliklarDelete")]
        public int DeleteSoft(int Id)
        {
            return _varlikDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarliklarRead, VarliklarLtd")]
        public List<EntityLayer.Concrete.Varlik.Varlik> GetListPagination(PagingParams pagingParams)
        {
            return _varlikDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _varlikDal.GetCount(filter);
        }

        public int GetCountDto(string filter = "")
        {
            return _varlikDal.GetCountDto(filter);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarliklarRead, VarliklarLtd")]
        public List<VarlikDto> GetListDto()
        {
            return _varlikDal.GetListDto();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, VarliklarRead, VarliklarLtd")]
        public List<VarlikDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _varlikDal.GetListPaginationDto(pagingParams);
        }

        //VarlikGrupID ye göre gelen Varlıklar için pagination
        [SecuredOperation(Roles = "Admin, RaporRead, Rapor1Read, VarliklarLtd")]
        public int GetCountDtoByVarlikGrupID(int VarlikGrupID, string filter = "")
        {
            return _varlikDal.GetCountDtoByVarlikGrupID(VarlikGrupID, filter);
        }

        //VarlikGrupID ye göre gelen Varlıkların count
        [SecuredOperation(Roles = "Admin, RaporRead, Rapor1Read, VarliklarLtd")]
        public List<VarlikDto> GetListPaginationDtoByVarlikGrupID(int VarlikGrupID,PagingParams pagingParams)
        {
            return _varlikDal.GetListPaginationDtoByVarlikGrupID(VarlikGrupID,pagingParams);
        }

        public List<string> AddListWithTransactionBySablon(List<EntityLayer.Concrete.Varlik.Varlik> listVarlik)
        {
            return _varlikDal.AddListWithTransactionBySablon(listVarlik);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<EntityLayer.Concrete.Varlik.Varlik> ExcelDataProcess(DataTable dataTable)
        {
            List<EntityLayer.Concrete.Varlik.Varlik> listVarlik = new List<EntityLayer.Concrete.Varlik.Varlik>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listVarlik.Add(new EntityLayer.Concrete.Varlik.Varlik()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    VarlikDurumID = row[2] .ToString() != "" ? Convert.ToInt32(row[2].ToString()) : 0,
                    VarlikTuruID = row[3] .ToString() != "" ? Convert.ToInt32(row[3].ToString()) : 0,
                    VarlikGrupID = row[4] .ToString() != "" ? Convert.ToInt32(row[4].ToString()) : 0,
                    BagliVarlikKod = row[5] .ToString() != "" ? Convert.ToInt32(row[5].ToString()) : 0,
                    KisimID = row[6] .ToString() != "" ? Convert.ToInt32(row[6].ToString()) : 0,
                    SarfYeriID = row[7] .ToString() != "" ? Convert.ToInt32(row[7].ToString()) : 0,
                    IsletmeID = row[8] .ToString() != "" ? Convert.ToInt32(row[8].ToString()) : 0,
                    MarkaID = row[9] .ToString() != "" ? Convert.ToInt32(row[9].ToString()) : 0,
                    ModelID = row[10] .ToString() != "" ? Convert.ToInt32(row[10].ToString()) : 0,
                    SeriNo = row[11].ToString(),
                    BarkodKod = row[12].ToString(),
                    GarantiBitisTarih = row[13] .ToString() != "" ? Convert.ToDateTime(row[5].ToString()) : DateTime.MaxValue,
                    SonKullanimTarih = row[14] .ToString() != "" ? Convert.ToDateTime(row[5].ToString()) : DateTime.MaxValue,
                    Aciklama = row[15].ToString(),
                    ZimmetliPersonelID = row[16] .ToString() != "" ? Convert.ToInt32(row[16].ToString()) : 0,
                    VarsayilanIsTipiID = row[17] .ToString() != "" ? Convert.ToInt32(row[17].ToString()) : 0,
                    VarsayilanBakimArizaID = row[18] .ToString() != "" ? Convert.ToInt32(row[18].ToString()) : 0,
                    VarsayilanArizaNedenID = row[19] .ToString() != "" ? Convert.ToInt32(row[19].ToString()) : 0,
                    VarsayilanArizaCozumID = row[20] .ToString() != "" ? Convert.ToInt32(row[20].ToString()) : 0,
                    EkipID = row[21] .ToString() != "" ? Convert.ToInt32(row[21].ToString()) : 0,
                    IsEmriTurID = row[22] .ToString() != "" ? Convert.ToInt32(row[22].ToString()) : 0,
                });
            }

            return listVarlik;
        }
    }
}