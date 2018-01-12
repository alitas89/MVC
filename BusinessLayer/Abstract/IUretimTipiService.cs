using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IUretimTipiService
    {
        UretimTipi GetById(int id);

        int Add(UretimTipi uretimtipi);

        int Update(UretimTipi uretimtipi);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}