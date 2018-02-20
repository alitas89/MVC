﻿using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IBakimRiskiService
    {
        List<BakimRiski> GetList();

        BakimRiski GetById(int id);

        int Add(BakimRiski bakimriski);

        int Update(BakimRiski bakimriski);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<BakimRiski> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}