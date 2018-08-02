using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System;
using System.Collections.Generic;
using System.Data;

namespace BusinessLayer.Concrete.Bakim
{
    public class IsEmriTuruManager : IIsEmriTuruService
    {
        IIsEmriTuruDal _isEmriTuruDal;

        public IsEmriTuruManager(IIsEmriTuruDal isEmriTuruDal)
        {
            _isEmriTuruDal = isEmriTuruDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, IsEmriTuruRead, IsEmriTuruLtd")]
        public List<IsEmriTuru> GetList()
        {
            return _isEmriTuruDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsEmriTuruRead, IsEmriTuruLtd")]
        public IsEmriTuru GetById(int Id)
        {
            return _isEmriTuruDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, IsEmriTuruCreate")]
        public int Add(IsEmriTuru ısemrituru)
        {
            return _isEmriTuruDal.Add(ısemrituru);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, IsEmriTuruUpdate")]
        public int Update(IsEmriTuru isEmriTuru)
        {
            return _isEmriTuruDal.Update(isEmriTuru);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, IsEmriTuruDelete")]
        public int Delete(int Id)
        {
            return _isEmriTuruDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, IsEmriTuruDelete")]
        public int DeleteSoft(int Id)
        {
            return _isEmriTuruDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsEmriTuruRead, IsEmriTuruLtd")]
        public List<IsEmriTuru> GetListPagination(PagingParams pagingParams)
        {
            return _isEmriTuruDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _isEmriTuruDal.GetCount(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<IsEmriTuru> listIsEmriTuru)
        {
            return _isEmriTuruDal.AddListWithTransactionBySablon(listIsEmriTuru);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<IsEmriTuru> ExcelDataProcess(DataTable dataTable)
        {
            List<IsEmriTuru> listIsEmriTuru = new List<IsEmriTuru>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listIsEmriTuru.Add(new IsEmriTuru()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    Aciklama = row[2].ToString(),
                    TekYilSayac = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    TekYilBaslangicSayaci = row[4] != DBNull.Value ? Convert.ToInt32(row[4].ToString()) : 0,
                    CiftYilSayac = row[5] != DBNull.Value ? Convert.ToInt32(row[5].ToString()) : 0,
                    CiftYilBaslangicSayaci = row[6] != DBNull.Value ? Convert.ToInt32(row[6].ToString()) : 0,
                    IsEmriVarsayilani = row[7] != DBNull.Value ? Convert.ToBoolean(row[7].ToString()) : false,
                    AksiyonIsEmriVarsayilani = row[8] != DBNull.Value ? Convert.ToBoolean(row[8].ToString()) : false,
                    KaizenIsEmriVarsayilani = row[9] != DBNull.Value ? Convert.ToBoolean(row[9].ToString()) : false,
                    IsTalepVarsayilani = row[10] != DBNull.Value ? Convert.ToBoolean(row[10].ToString()) : false,
                    PeriyodikBakimVarsayilani = row[11] != DBNull.Value ? Convert.ToBoolean(row[11].ToString()) : false,
                    Servis = row[12] != DBNull.Value ? Convert.ToBoolean(row[12].ToString()) : false,
                    SokmeTakma = row[13] != DBNull.Value ? Convert.ToBoolean(row[13].ToString()) : false,
                    BagliDokumanlar = row[14] != DBNull.Value ? Convert.ToBoolean(row[14].ToString()) : false,
                    Hurdalar = row[15] != DBNull.Value ? Convert.ToBoolean(row[15].ToString()) : false,
                    SayacDegerleri = row[16] != DBNull.Value ? Convert.ToBoolean(row[16].ToString()) : false,
                    IsAdimlari = row[17] != DBNull.Value ? Convert.ToBoolean(row[17].ToString()) : false,
                    BakimNoktalari = row[18] != DBNull.Value ? Convert.ToBoolean(row[18].ToString()) : false,
                    SeyahatBilgileri = row[19] != DBNull.Value ? Convert.ToBoolean(row[19].ToString()) : false,
                    EtkilenenVarliklar = row[20] != DBNull.Value ? Convert.ToBoolean(row[20].ToString()) : false,
                    Icerik = row[21] != DBNull.Value ? Convert.ToBoolean(row[21].ToString()) : false,
                    GrupOzellikleri = row[22] != DBNull.Value ? Convert.ToBoolean(row[22].ToString()) : false,
                    OlcumDegeri = row[23] != DBNull.Value ? Convert.ToBoolean(row[23].ToString()) : false,
                    IsGunlugu = row[24] != DBNull.Value ? Convert.ToBoolean(row[24].ToString()) : false,
                    BakimRiski = row[25] != DBNull.Value ? Convert.ToBoolean(row[25].ToString()) : false,
                    ArizaKodları = row[26] != DBNull.Value ? Convert.ToBoolean(row[26].ToString()) : false,
                    NedenAnalizi = row[27] != DBNull.Value ? Convert.ToBoolean(row[27].ToString()) : false,
                    OzelKodlar = row[28] != DBNull.Value ? Convert.ToBoolean(row[28].ToString()) : false,
                    KullanilanAracGerecler = row[29] != DBNull.Value ? Convert.ToBoolean(row[29].ToString()) : false,
                });
            }

            return listIsEmriTuru;
        }

    }
}
