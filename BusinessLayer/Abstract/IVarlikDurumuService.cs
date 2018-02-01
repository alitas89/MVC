﻿using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IVarlikDurumuService
    {
        List<VarlikDurumu> GetList();

        VarlikDurumu GetById(int id);

        int Add(VarlikDurumu varlikdurumu);

        int Update(VarlikDurumu varlikdurumu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<VarlikDurumu> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}