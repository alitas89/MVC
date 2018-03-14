﻿using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IIsTalebiService
    {
        List<IsTalebi> GetList();

        IsTalebi GetById(int id);

        int Add(IsTalebi istalebi);

        int Update(IsTalebi istalebi);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<IsTalebi> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<IsTalebiDto> GetListPaginationDto(PagingParams pagingParams);

        int GetCountDto(string filter = "");
    }
}
