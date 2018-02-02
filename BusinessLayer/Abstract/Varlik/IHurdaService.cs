using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IHurdaService
    {
        List<Hurda> GetList();

        Hurda GetById(int id);

        int Add(Hurda hurda);

        int Update(Hurda hurda);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Hurda> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}