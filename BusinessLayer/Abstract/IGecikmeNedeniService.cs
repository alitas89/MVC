using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IGecikmeNedeniService
    {
        List<GecikmeNedeni> GetList();

        GecikmeNedeni GetById(int id);

        int Add(GecikmeNedeni gecikmenedeni);

        int Update(GecikmeNedeni gecikmenedeni);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}