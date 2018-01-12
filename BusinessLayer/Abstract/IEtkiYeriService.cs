using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IEtkiYeriService
    {
        EtkiYeri GetById(int id);

        int Add(EtkiYeri etkiyeri);

        int Update(EtkiYeri etkiyeri);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}