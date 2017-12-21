using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IIsletmeService
    {
        Isletme GetById(int id);

        int Add(Isletme ısletme);

        int Update(Isletme ısletme);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}