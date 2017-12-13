using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IRolService
    {
        Rol GetById(int id);

        int Add(Rol rol);

        int Update(Rol rol);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}