using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IStatuService
    {
        Statu GetById(int id);

        int Add(Statu statu);

        int Update(Statu statu);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}