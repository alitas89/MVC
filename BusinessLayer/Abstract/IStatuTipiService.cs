using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IStatuTipiService
    {
        List<StatuTipi> GetList();

        StatuTipi GetById(int id);

        int Add(StatuTipi statutipi);

        int Update(StatuTipi statutipi);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}