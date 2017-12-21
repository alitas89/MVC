using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IMarkaService
    {
        Marka GetById(int id);

        int Add(Marka marka);

        int Update(Marka marka);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}