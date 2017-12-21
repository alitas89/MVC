using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IHurdaService
    {
        Hurda GetById(int id);

        int Add(Hurda hurda);

        int Update(Hurda hurda);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}