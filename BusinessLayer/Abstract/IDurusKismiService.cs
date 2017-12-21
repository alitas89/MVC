using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IDurusKismiService
    {
        DurusKismi GetById(int id);

        int Add(DurusKismi duruskismi);

        int Update(DurusKismi duruskismi);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}