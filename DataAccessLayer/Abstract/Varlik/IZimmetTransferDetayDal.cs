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

        int GetCountDto(int ZimmetTransferID, string filter= "");

        int UpdateVarlikZimmet(int VarlikID, int ZimmetliPersonelID);

        int GetZimmetliPersonel(int ZimmetTransferID);

        int AddWithTransaction(int ZimmetTransferID, List<ZimmetTransferDetay> listZimmetTransferDetay);

        int UpdateWithTransaction(int ZimmetTransferID, List<ZimmetTransferDetayDto> listZimmetTransferDetay);
    }
}