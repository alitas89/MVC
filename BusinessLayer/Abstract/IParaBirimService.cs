using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IParaBirimService
    {
        ParaBirim GetById(int id);

        int Add(ParaBirim parabirim);

        int Update(ParaBirim parabirim);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}