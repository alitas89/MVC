using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IHurdaService
    {
        List<Hurda> GetList();

        Hurda GetById(int id);

        int Add(Hurda hurda);

        int Update(Hurda hurda);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}