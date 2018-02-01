﻿using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IBeklemeIptalNedeniService
    {
        List<BeklemeIptalNedeni> GetList();

        BeklemeIptalNedeni GetById(int id);

        int Add(BeklemeIptalNedeni beklemeıptalnedeni);

        int Update(BeklemeIptalNedeni beklemeıptalnedeni);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<BeklemeIptalNedeni> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");

    }
}