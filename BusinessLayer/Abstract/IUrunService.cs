using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IUrunService
    {
        List<Urun> GetList();

        Urun GetById(int id);

        int Add(Urun urun);

        int Update(Urun urun);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}