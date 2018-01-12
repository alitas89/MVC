using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IOncelikService
    {
        Oncelik GetById(int id);

        int Add(Oncelik oncelik);

        int Update(Oncelik oncelik);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}