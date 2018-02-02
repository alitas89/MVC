using System.Collections.Generic;
using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Abstract.Genel
{
    public interface IRolService
    {
        Rol GetById(int id);

        int Add(Rol rol);

        int Update(Rol rol);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Rol> GetRolByKullaniciId(int kullaniciId);
    }
}