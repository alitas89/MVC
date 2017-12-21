using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IKisimService
    {
        Kisim GetById(int id);

        int Add(Kisim kisim);

        int Update(Kisim kisim);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}