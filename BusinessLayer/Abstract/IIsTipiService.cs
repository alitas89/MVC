using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IIsTipiService
    {
        IsTipi GetById(int id);

        int Add(IsTipi isTipi);

        int Update(IsTipi isTipi);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}