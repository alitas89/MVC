using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IZimmetTransferDetayDal : IEntityRepository<ZimmetTransferDetay>
    {
        List<ZimmetTransferDetayDto> GetList(int ZimmetTransferID);

        List<ZimmetTransferDetayDto> GetListPaginationDto(int ZimmetTransferID, PagingParams pagingParams);

        int GetCountDto(int ZimmetTransferID, string filterCol = "", string filterVal = "");

        int UpdateVarlikZimmet(int ZimmetTransferID, int VarlikID);
    }
}