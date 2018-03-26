﻿using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete.Genel;

namespace DataAccessLayer.Abstract.Genel
{
    public interface IYetkiGrupRolDal : IEntityRepository<YetkiGrupRol>
    {
        List<YetkiGrupRol> GetListByGrupId(int YetkiGrupID);

        List<YetkiGrupRol> GetYetkiRolByYetkiGrupID(int YetkiGrupID);

        int DeleteSoftByYetkiGrupID(int YetkiGrupID);
    }
}