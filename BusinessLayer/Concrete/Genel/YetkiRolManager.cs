﻿using System.Collections.Generic;
using BusinessLayer.Abstract.Genel;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Concrete.Genel
{
    public class YetkiRolManager : IYetkiRolService
    {
        IYetkiRolDal _yetkirolDal;

        public YetkiRolManager(IYetkiRolDal yetkirolDal)
        {
            _yetkirolDal = yetkirolDal;
        }

        [SecuredOperation(Roles = "Admin, SistemRead, YetkiGrupRead, YetkiGrupLtd")]
        public List<YetkiRol> GetList()
        {
            return _yetkirolDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, SistemRead, YetkiGrupRead, YetkiGrupLtd")]
        public YetkiRol GetById(int Id)
        {
            return _yetkirolDal.Get(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemCreate, YetkiGrupCreate")]
        public int Add(YetkiRol yetkirol)
        {
            return _yetkirolDal.Add(yetkirol);
        }

        [SecuredOperation(Roles = "Admin, SistemUpdate, YetkiGrupUpdate")]
        public int Update(YetkiRol yetkirol)
        {
            return _yetkirolDal.Update(yetkirol);
        }

        [SecuredOperation(Roles = "Admin, SistemDelete, YetkiGrupDelete")]
        public int Delete(int Id)
        {
            return _yetkirolDal.Delete(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemDelete, YetkiGrupDelete")]
        public int DeleteSoft(int Id)
        {
            return _yetkirolDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, SistemRead, YetkiGrupRead, YetkiGrupLtd")]
        public List<YetkiRol> GetListPagination(PagingParams pagingParams)
        {
            return _yetkirolDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _yetkirolDal.GetCount(filter);
        }

    }
}