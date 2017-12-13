using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IRolDal : IEntityRepository<Rol>
    {
        List<Rol> GetRolByKullaniciId(int kullaniciId);
    }
}