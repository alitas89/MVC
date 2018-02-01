using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IArizaNedeniGrubuService
    {
        List<ArizaNedeniGrubu> GetList();

        ArizaNedeniGrubu GetById(int id);

        int Add(ArizaNedeniGrubu arizanedenigrubu);

        int Update(ArizaNedeniGrubu arizanedenigrubu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<ArizaNedeniGrubu> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}