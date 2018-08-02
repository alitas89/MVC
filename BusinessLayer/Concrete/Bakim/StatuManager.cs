using System;
using System.Collections.Generic;
using System.Data;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class StatuManager : IStatuService
    {
        IStatuDal _statuDal;

        public StatuManager(IStatuDal statuDal)
        {
            _statuDal = statuDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, StatuRead, StatuLtd")]
        public List<Statu> GetList()
        {
            return _statuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, StatuRead, StatuLtd")]
        public Statu GetById(int Id)
        {
            return _statuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, StatuCreate")]
        public int Add(Statu statu)
        {
            return _statuDal.Add(statu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, StatuUpdate")]
        public int Update(Statu statu)
        {
            return _statuDal.Update(statu);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, StatuDelete")]
        public int Delete(int Id)
        {
            return _statuDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, StatuDelete")]
        public int DeleteSoft(int Id)
        {
            return _statuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, StatuRead, StatuLtd")]
        public List<Statu> GetListPagination(PagingParams pagingParams)
        {
            return _statuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _statuDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, StatuRead, StatuLtd")]
        public List<StatuDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _statuDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _statuDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<Statu> listStatu)
        {
            return _statuDal.AddListWithTransactionBySablon(listStatu);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<Statu> ExcelDataProcess(DataTable dataTable)
        {
            List<Statu> listStatu = new List<Statu>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listStatu.Add(new Statu()
                {
                    StatuTipiID = row[0] != DBNull.Value ? Convert.ToInt32(row[0].ToString()) : 0,
                    Kod = row[1].ToString(),
                    Ad = row[2].ToString(),
                    VarlikDurumuID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    KaynakSinifi1ID = row[4] != DBNull.Value ? Convert.ToInt32(row[4].ToString()) : 0,
                    KaynakSinifi2ID = row[5] != DBNull.Value ? Convert.ToInt32(row[5].ToString()) : 0,
                    KaynakSinifi3ID = row[6] != DBNull.Value ? Convert.ToInt32(row[6].ToString()) : 0,
                    Aciklama = row[7].ToString(),
                    BeklemeIptalNedeni = row[8] != DBNull.Value ? Convert.ToBoolean(row[8].ToString()) : false,
                    TalepVarsayilani = row[9] != DBNull.Value ? Convert.ToBoolean(row[9].ToString()) : false,
                    TalepOnay = row[10] != DBNull.Value ? Convert.ToBoolean(row[10].ToString()) : false,
                    TalepRed = row[11] != DBNull.Value ? Convert.ToBoolean(row[11].ToString()) : false,
                    EmirVarsayilani = row[12] != DBNull.Value ? Convert.ToBoolean(row[12].ToString()) : false,
                    IsEmriAcik = row[13] != DBNull.Value ? Convert.ToBoolean(row[13].ToString()) : false,
                    IsEmriKapali = row[14] != DBNull.Value ? Convert.ToBoolean(row[14].ToString()) : false,
                    IsEmriIptal = row[15] != DBNull.Value ? Convert.ToBoolean(row[15].ToString()) : false,
                    EkipmanCalismiyor = row[16] != DBNull.Value ? Convert.ToBoolean(row[16].ToString()) : false,
                    PlanlanmisIsEmri = row[17] != DBNull.Value ? Convert.ToBoolean(row[17].ToString()) : false,
                    IsEmrineBaslandi = row[18] != DBNull.Value ? Convert.ToBoolean(row[18].ToString()) : false,
                    IsEmriTamamlandi = row[19] != DBNull.Value ? Convert.ToBoolean(row[19].ToString()) : false,
                    IsTeslimEdildi = row[20] != DBNull.Value ? Convert.ToBoolean(row[20].ToString()) : false,
                    SorumluDegisti = row[21] != DBNull.Value ? Convert.ToBoolean(row[21].ToString()) : false,
                    BakimErtelendi = row[22] != DBNull.Value ? Convert.ToBoolean(row[22].ToString()) : false,
                    BakimDevamEdiyor = row[23] != DBNull.Value ? Convert.ToBoolean(row[23].ToString()) : false,
                    BildirimIslemleriniYoksay = row[24] != DBNull.Value ? Convert.ToBoolean(row[24].ToString()) : false,
                    KismiSatinalmaTalebiOlusturuldu = row[25] != DBNull.Value ? Convert.ToBoolean(row[25].ToString()) : false,
                    SatinalmaTalebiOlusturuldu = row[26] != DBNull.Value ? Convert.ToBoolean(row[26].ToString()) : false,
                    SatinalmaTeklifVerildi = row[27] != DBNull.Value ? Convert.ToBoolean(row[27].ToString()) : false,
                    SatinalmaTeklifDegerlendirildi = row[28] != DBNull.Value ? Convert.ToBoolean(row[28].ToString()) : false,
                    SatinalmaSiparisVerildi = row[29] != DBNull.Value ? Convert.ToBoolean(row[29].ToString()) : false,
                    MalzemelerinSatinalmaFiyatBelirlendi = row[30] != DBNull.Value ? Convert.ToBoolean(row[30].ToString()) : false,
                    SatinalmaAmbarGirisiYapildi = row[31] != DBNull.Value ? Convert.ToBoolean(row[31].ToString()) : false,
                    EpostaGonder = row[32] != DBNull.Value ? Convert.ToBoolean(row[32].ToString()) : false,
                    SMSGonder = row[33] != DBNull.Value ? Convert.ToBoolean(row[33].ToString()) : false,
                    HerKayitAsamasindaUygula = row[34] != DBNull.Value ? Convert.ToBoolean(row[34].ToString()) : false,
                    KaydiKilitle = row[35] != DBNull.Value ? Convert.ToBoolean(row[35].ToString()) : false,
                });
            }

            return listStatu;
        }
    }
}