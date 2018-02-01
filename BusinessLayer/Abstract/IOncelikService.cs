using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IOncelikService
    {
        List<Oncelik> GetList();

        Oncelik GetById(int id);

        int Add(Oncelik oncelik);

        int Update(Oncelik oncelik);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Oncelik> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}