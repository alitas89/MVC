using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IOzNitelikService
    {
        List<OzNitelik> GetList();

        OzNitelik GetById(int id);

        int Add(OzNitelik oznitelik);

        int Update(OzNitelik oznitelik);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<OzNitelik> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<OzNitelikDto> GetList(int VarlikSablonID);

        List<OzNitelikDto> GetListByVarlikTuruID(int VarlikTuruID);

        List<OzNitelikDto> GetListPaginationDto(int VarlikSablonID, PagingParams pagingParams);

        int GetCountDto(int VarlikSablonID, string filter = "");

        int AddOzNitelik(int varlikSablonID, string arrOzNitelik);

        int UpdateOzNitelik(int varlikSablonID, string arrOzNitelik);
    }
}
