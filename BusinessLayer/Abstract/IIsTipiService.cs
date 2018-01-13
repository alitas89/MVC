using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IIsTipiService
    {
        List<IsTipi> GetList();

        IsTipi GetById(int id);

        int Add(IsTipi isTipi);

        int Update(IsTipi isTipi);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}