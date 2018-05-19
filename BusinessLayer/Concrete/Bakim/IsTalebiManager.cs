using System;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using EntityLayer.ComplexTypes.DtoModel.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class IsTalebiManager : IIsTalebiService
    {
        IIsTalebiDal _isTalebiDal;

        public IsTalebiManager(IIsTalebiDal ıstalebiDal)
        {
            _isTalebiDal = ıstalebiDal;
        }


        [SecuredOperation(Roles = "Admin, BakimRead, IsTalebiRead, IsTalebiLtd")]
        public List<IsTalebi> GetList()
        {
            return _isTalebiDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsTalebiRead, IsTalebiLtd")]
        public IsTalebi GetById(int Id)
        {
            return _isTalebiDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]

        [SecuredOperation(Roles = "Admin, BakimCreate, IsTalebiCreate")]
        public int Add(IsTalebi ıstalebi)
        {
            ıstalebi.TalepYil = DateTime.Now.Year;
            ıstalebi.StatuID = 7; //Yeni iş talebi atanır
            return _isTalebiDal.Add(ıstalebi);
        }


        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [SecuredOperation(Roles = "Admin, BakimUpdate, IsTalebiUpdate")]
        public int Update(IsTalebiIsEmriNoDto istalebi)
        {
            //Eğer Statü Kodu 8 (Onayla) Olan Bir Güncelleme Geldiyse Aynı Zamanda Bir İş Emri De Oluşmalı!
            if (istalebi.StatuID == 8)
            {
                //İş Emri de oluşmalı
                return _isTalebiDal.UpdateWithTransactionForCreateIsEmri(istalebi);
            }

            //NormalDurum
            return _isTalebiDal.Update(istalebi);
        }


        [SecuredOperation(Roles = "Admin, BakimDelete, IsTalebiDelete")]
        public int Delete(int Id)
        {
            return _isTalebiDal.Delete(Id);
        }


        [SecuredOperation(Roles = "Admin, BakimDelete, IsTalebiDelete")]
        public int DeleteSoft(int Id)
        {
            return _isTalebiDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsTalebiRead, IsTalebiLtd")]
        public List<IsTalebi> GetListPagination(PagingParams pagingParams)
        {
            return _isTalebiDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _isTalebiDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsTalebiRead, IsTalebiLtd")]
        public List<IsTalebiDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _isTalebiDal.GetListPaginationDto(pagingParams);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsTalebiRead, IsTalebiLtd")]
        public List<IsTalebiDto> GetListPaginationDtoByKullaniciID(PagingParams pagingParams, int kullaniciID)
        {
            return _isTalebiDal.GetListPaginationDtoByKullaniciID(pagingParams, kullaniciID);
        }

        public int GetCountDto(string filter = "")
        {
            return _isTalebiDal.GetCountDto(filter);
        }

        public int GetCountDtoByKullaniciID(int KullaniciID, string filter = "")
        {
            return _isTalebiDal.GetCountDtoByKullaniciID(KullaniciID, filter);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsTalebiRead, IsTalebiLtd")]
        public List<IsTipiForKullaniciTemp> GetIsTipiListByKullaniciID(int KullaniciID)
        {
            return _isTalebiDal.GetIsTipiListByKullaniciID(KullaniciID);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsTalebiRead, IsTalebiLtd")]
        public List<EmirTuruIsTipiTemp> GetEmirTuruListByIsTipiID(int IsTipiID)
        {
            return _isTalebiDal.GetEmirTuruListByIsTipiID(IsTipiID);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, IsTalebiRead, IsTalebiLtd")]
        public List<IsEmriNo> GetIsEmriNoByIsTalepID(int IsTalepID)
        {
            return _isTalebiDal.GetIsEmriNoByIsTalepID(IsTalepID);
        }


        [SecuredOperation(Roles = "Admin, BakimCreate, IsTalebiCreate")]
        public int AddWithTransaction(IsTalebi ıstalebi)
        {
            ıstalebi.TalepYil = DateTime.Now.Year;
            ıstalebi.StatuID = 7; //Yeni iş talebi atanır
            return _isTalebiDal.AddWithTransaction(ıstalebi);
        }
    }
}
