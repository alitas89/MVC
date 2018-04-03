using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IOzNitelikDal : IEntityRepository<OzNitelik>
    {
        List<OzNitelikDto> GetList(int VarlikSablonID);

        List<OzNitelikDto> GetListByVarlikTuruID(int VarlikTuruID);

        List<OzNitelikDto> GetListPaginationDto(int VarlikSablonID, PagingParams pagingParams);

        int GetCountDto(int VarlikSablonID, string filter = "");
    }
}
