using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Varlik;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IVarlikSablonDal : IEntityRepository<VarlikSablon>
    {       
        List<VarlikSablonDto> GetListPaginationDto(PagingParams pagingParams);       

        int GetCountDto(string filter = "");

        bool IsSablonDefined(int VarlikTuruID);
    }
}
