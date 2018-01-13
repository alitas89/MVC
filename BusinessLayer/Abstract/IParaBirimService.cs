using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IParaBirimService
    {
        List<ParaBirim> GetList();

        ParaBirim GetById(int id);

        int Add(ParaBirim parabirim);

        int Update(ParaBirim parabirim);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}