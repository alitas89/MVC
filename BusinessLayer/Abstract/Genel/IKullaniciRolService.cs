using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Abstract.Genel
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