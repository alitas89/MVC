﻿using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IGonderimFormatiService
    {
        List<GonderimFormati> GetList();

        GonderimFormati GetById(int id);

        int Add(GonderimFormati gonderimformati);

        int Update(GonderimFormati gonderimformati);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<GonderimFormati> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}