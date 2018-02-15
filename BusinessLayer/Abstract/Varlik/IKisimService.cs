﻿using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IKisimService
    {
        List<Kisim> GetList();

        List<Kisim> GetList(int SarfYeriID);

        Kisim GetById(int id);

        int Add(Kisim kisim);

        int Update(Kisim kisim);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<KisimDto> GetListDto();

        List<Kisim> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}