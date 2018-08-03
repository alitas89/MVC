using BusinessLayer.Abstract.Personel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Personel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Personel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using System.Data;
using System;

namespace BusinessLayer.Concrete.Personel
{
    public class KaynakManager : IKaynakService
    {
        IKaynakDal _kaynakDal;

        public KaynakManager(IKaynakDal kaynakDal)
        {
            _kaynakDal = kaynakDal;
        }

        
        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakRead, KaynakLtd")]
        public List<Kaynak> GetList()
        {
            return _kaynakDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakRead, KaynakLtd")]
        public Kaynak GetById(int Id)
        {
            return _kaynakDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelCreate, KaynakCreate")]
        public int Add(Kaynak kaynak)
        {
            return _kaynakDal.Add(kaynak);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, PersonelUpdate, KaynakUpdate")]
        public int Update(Kaynak kaynak)
        {
            return _kaynakDal.Update(kaynak);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, KaynakDelete")]
        public int Delete(int Id)
        {
            return _kaynakDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, PersonelDelete, KaynakDelete")]
        public int DeleteSoft(int Id)
        {
            return _kaynakDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakRead, KaynakLtd")]
        public List<Kaynak> GetListPagination(PagingParams pagingParams)
        {
            return _kaynakDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _kaynakDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, PersonelRead, KaynakRead, KaynakLtd")]
        public List<KaynakDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _kaynakDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _kaynakDal.GetCountDto(filter);
        }

        public List<Kaynak> GetListKaynakHaveKullaniciID()
        {
            return _kaynakDal.GetListKaynakHaveKullaniciID();
        }

        public List<string> AddListWithTransactionBySablon(List<Kaynak> listKaynak)
        {
            return _kaynakDal.AddListWithTransactionBySablon(listKaynak);
        }

        //*Excel içeriğinde bulunan verileri veritabanına kayıt atar
        public List<Kaynak> ExcelDataProcess(DataTable dataTable)
        {
            List<Kaynak> listKaynak = new List<Kaynak>();
            for (int i = 1; i < dataTable.Rows.Count; i++)
            {
                var row = dataTable.Rows[i].ItemArray;
                //Eklenecek veriler
                listKaynak.Add(new Kaynak()
                {
                    Kod = row[0].ToString(),
                    Ad = row[1].ToString(),
                    KisimID = row[2] != DBNull.Value ? Convert.ToInt32(row[2].ToString()) : 0,
                    SarfyeriID = row[3] != DBNull.Value ? Convert.ToInt32(row[3].ToString()) : 0,
                    IsletmeID = row[4] != DBNull.Value ? Convert.ToInt32(row[4].ToString()) : 0,
                    VarlikID = row[5] != DBNull.Value ? Convert.ToInt32(row[5].ToString()) : 0,
                    EkipID = row[6] != DBNull.Value ? Convert.ToInt32(row[6].ToString()) : 0,
                    KaynakSinifID = row[7] != DBNull.Value ? Convert.ToInt32(row[7].ToString()) : 0,
                    VardiyaID = row[8] != DBNull.Value ? Convert.ToInt32(row[8].ToString()) : 0,
                    StatuID = row[9] != DBNull.Value ? Convert.ToInt32(row[9].ToString()) : 0,
                    AmbarID = row[10] != DBNull.Value ? Convert.ToInt32(row[10].ToString()) : 0,
                    KaynakPozisyonuID = row[11] != DBNull.Value ? Convert.ToInt32(row[11].ToString()) : 0,
                    DurusIsTipiID = row[12] != DBNull.Value ? Convert.ToInt32(row[12].ToString()) : 0,
                    KaynakTipiID = row[13] != DBNull.Value ? Convert.ToInt32(row[13].ToString()) : 0,
                    KaynakDurumuID = row[14] != DBNull.Value ? Convert.ToInt32(row[14].ToString()) : 0,
                    KaynakTuruID = row[15] != DBNull.Value ? Convert.ToInt32(row[15].ToString()) : 0,
                    Email = row[16].ToString(),
                    TelefonNo = row[17].ToString(),
                });
            }


            return listKaynak;
        }
    }
}
