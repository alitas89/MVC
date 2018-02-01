﻿using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IUrunService
    {
        List<Urun> GetList();

        Urun GetById(int id);

        int Add(Urun urun);

        int Update(Urun urun);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Urun> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}