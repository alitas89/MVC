using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ISarfYeriService
    {
        List<SarfYeri> GetList();

        SarfYeri GetById(int id);

        int Add(SarfYeri sarfyeri);

        int Update(SarfYeri sarfyeri);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}