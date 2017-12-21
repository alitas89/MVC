using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IVarlikTuruService
    {
        VarlikTuru GetById(int id);

        int Add(VarlikTuru varlikturu);

        int Update(VarlikTuru varlikturu);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}