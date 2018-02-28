﻿using System.Collections.Generic;
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

        List<EntityLayer.Concrete.Varlik.Varlik> GetList(int KisimID);

        int GetCountDto(string filterCol = "", string filterVal = "");

        bool IsKodDefined(string Kod);
    }
}
