using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IUretimTipiService
    {
        List<UretimTipi> GetList();

        UretimTipi GetById(int id);

        int Add(UretimTipi uretimtipi);

        int Update(UretimTipi uretimtipi);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}