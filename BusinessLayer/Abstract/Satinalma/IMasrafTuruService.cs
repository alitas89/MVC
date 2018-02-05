using System.Collections.Generic;
using EntityLayer.Concrete.Satinalma;
using EntityLayer.ComplexTypes.ParameterModel;

namespace BusinessLayer.Abstract.Satinalma
{
    public interface IMasrafTuruService
    {
        List<MasrafTuru> GetList();

        MasrafTuru GetById(int id);

        int Add(MasrafTuru masrafturu);

        int Update(MasrafTuru masrafturu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<MasrafTuru> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}
