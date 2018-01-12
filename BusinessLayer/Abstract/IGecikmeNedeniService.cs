using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IGecikmeNedeniService
    {
        GecikmeNedeni GetById(int id);

        int Add(GecikmeNedeni gecikmenedeni);

        int Update(GecikmeNedeni gecikmenedeni);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}