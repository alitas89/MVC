using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
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

        List<VarlikTuru> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}