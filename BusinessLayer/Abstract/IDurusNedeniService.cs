using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IDurusNedeniService
    {
        List<DurusNedeni> GetList();

        DurusNedeni GetById(int id);

        int Add(DurusNedeni durusnedeni);

        int Update(DurusNedeni durusnedeni);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}