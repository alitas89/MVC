using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IVarlikTuruService
    {
        List<VarlikTuru> GetList();

        VarlikTuru GetById(int id);

        int Add(VarlikTuru varlikturu);

        int Update(VarlikTuru varlikturu);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}