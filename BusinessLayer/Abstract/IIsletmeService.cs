using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IIsletmeService
    {
        List<Isletme> GetList();

        Isletme GetById(int id);

        int Add(Isletme ısletme);

        int Update(Isletme ısletme);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Isletme> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}