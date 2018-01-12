using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IHizmetService
    {
        Hizmet GetById(int id);

        int Add(Hizmet hizmet);

        int Update(Hizmet hizmet);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}