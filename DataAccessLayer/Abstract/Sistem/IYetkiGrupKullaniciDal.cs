using System;
using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Abstract.Sistem
{
    public interface IYetkiGrupKullaniciDal : IEntityRepository<YetkiGrupKullanici>
    {
        List<YetkiGrupKullanici> GetListByKullaniciId(int kullaniciId);

        int DeleteSoftByKullaniciId(int Id);

        int AddWithTransaction(int kullaniciId, Array arrYetki);        
    }
}