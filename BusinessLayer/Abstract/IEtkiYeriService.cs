using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IEtkiYeriService
    {
        List<EtkiYeri> GetList();

        EtkiYeri GetById(int id);

        int Add(EtkiYeri etkiyeri);

        int Update(EtkiYeri etkiyeri);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}