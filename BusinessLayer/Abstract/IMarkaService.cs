using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IMarkaService
    {
        List<Marka> GetList();

        Marka GetById(int id);

        int Add(Marka marka);

        int Update(Marka marka);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}