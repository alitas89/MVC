using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IVarlikOzNitelikService
    {
        List<VarlikOzNitelik> GetList();

        VarlikOzNitelik GetById(int id);

        int Add(VarlikOzNitelik varlikoznitelik);

        int Update(VarlikOzNitelik varlikoznitelik);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<VarlikOzNitelik> GetListByVarlikID(int VarlikID);

        List<VarlikOzNitelik> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        int AddVarlikOzNitelik(int varlikID, string arrVarlikOzNitelik);

        int UpdateVarlikOzNitelik(int varlikID, string arrVarlikOzNitelik);
    }
}
