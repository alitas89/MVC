using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IHizmetService
    {
        List<Hizmet> GetList();

        Hizmet GetById(int id);

        int Add(Hizmet hizmet);

        int Update(Hizmet hizmet);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Hizmet> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}