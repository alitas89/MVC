using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IMarkaService
    {
        List<Marka> GetList();

        Marka GetById(int id);

        int Add(Marka marka);

        int Update(Marka marka);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Marka> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");

    }
}