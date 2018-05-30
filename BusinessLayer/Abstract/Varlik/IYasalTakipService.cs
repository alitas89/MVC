using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IYasalTakipService
    {
        List<YasalTakip> GetList();

        YasalTakip GetById(int id);

        int Add(YasalTakip yasaltakip);

        int Update(YasalTakip yasaltakip);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<YasalTakip> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");

        YasalTakip GetYasalTakipByVarlikID(int VarlikID);
    }

}