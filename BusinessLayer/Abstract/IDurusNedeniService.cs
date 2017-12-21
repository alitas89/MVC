using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IDurusNedeniService
    {
        DurusNedeni GetById(int id);

        int Add(DurusNedeni durusnedeni);

        int Update(DurusNedeni durusnedeni);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}