using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IVarlikDurumuService
    {
        VarlikDurumu GetById(int id);

        int Add(VarlikDurumu varlikdurumu);

        int Update(VarlikDurumu varlikdurumu);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}