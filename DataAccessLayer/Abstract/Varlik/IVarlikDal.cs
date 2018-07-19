using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IVarlikDal : IEntityRepository<EntityLayer.Concrete.Varlik.Varlik>
    {
        List<VarlikDto> GetListDto();

        List<VarlikDto> GetListPaginationDto(PagingParams pagingParams);

        List<EntityLayer.Concrete.Varlik.Varlik> GetListByKisimID(int KisimID);

        List<EntityLayer.Concrete.Varlik.Varlik> GetListByKaynakID(int KaynakID);

        int GetCountDto(string filter = "");

        bool IsKodDefined(string Kod);

        List<string> AddListWithTransactionBySablon(List<EntityLayer.Concrete.Varlik.Varlik> listVarlik);

    }
}
