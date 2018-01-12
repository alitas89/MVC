using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IBeklemeIptalNedeniService
    {
        BeklemeIptalNedeni GetById(int id);

        int Add(BeklemeIptalNedeni beklemeıptalnedeni);

        int Update(BeklemeIptalNedeni beklemeıptalnedeni);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}