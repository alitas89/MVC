using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IVarlikGrupDal : IEntityRepository<VarlikGrup>
    {
        List<VarlikGrupDto> GetListDto();
    }
}