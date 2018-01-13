using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IBakimOncelikService
    {
        List<BakimOncelik> GetList();

        BakimOncelik GetById(int id);

        int Add(BakimOncelik bakimoncelik);

        int Update(BakimOncelik bakimoncelik);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}