using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IKullaniciService
    {
        Kullanici GetById(int id);

        int Add(Kullanici kullanici);

        int Update(Kullanici kullanici);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}