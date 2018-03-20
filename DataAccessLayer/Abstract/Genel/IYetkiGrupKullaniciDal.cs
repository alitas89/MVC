using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete.Genel;

namespace DataAccessLayer.Abstract.Genel
{
    public interface IYetkiGrupKullaniciDal : IEntityRepository<YetkiGrupKullanici>
    {
        List<YetkiGrupKullanici> GetListByKullaniciId(int kullaniciId);

        int DeleteSoftByKullaniciId(int Id);
    }
}