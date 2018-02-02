using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete.Genel;

namespace DataAccessLayer.Abstract.Genel
{
    public interface IRolDal : IEntityRepository<Rol>
    {
        List<Rol> GetRolByKullaniciId(int kullaniciId);
    }
}