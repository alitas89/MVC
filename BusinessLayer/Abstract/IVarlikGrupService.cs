using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IVarlikGrupService
    {
        VarlikGrup GetById(int id);

        int Add(VarlikGrup varlikgrup);

        int Update(VarlikGrup varlikgrup);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}