using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IBirimService
    {
        List<Birim> GetList();

        Birim GetById(int id);

        int Add(Birim birim);

        int Update(Birim birim);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}