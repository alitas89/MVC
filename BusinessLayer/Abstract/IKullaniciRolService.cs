using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IKullaniciRolService
    {
        KullaniciRol GetById(int id);

        int Add(KullaniciRol kullanicirol);

        int Update(KullaniciRol kullanicirol);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}