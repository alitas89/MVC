using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;

namespace BusinessLayer.Abstract.Malzeme
{
    public interface IMalzemeSeriNoService
    {
        List<MalzemeSeriNo> GetList();

        MalzemeSeriNo GetById(int id);

        int Add(MalzemeSeriNo malzemeserino);

        int Update(MalzemeSeriNo malzemeserino);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<MalzemeSeriNo> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}