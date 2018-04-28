﻿using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IIsTipiEmirTuruDal : IEntityRepository<IsTipiEmirTuru>
    {
        int AddWithTransaction(int IsTipiID, List<IsTipiEmirTuru> listIsTipiEmirTuru);

        int UpdateWithTransaction(int IsTipiID, List<IsTipiEmirTuruDto> listIsTipiEmirTuru);

        List<IsTipiEmirTuruDto> GetListPaginationDto(int IsTipiID, PagingParams pagingParams);

        int GetCountDto(int IsTipiID, string filter = "");

    }
}
