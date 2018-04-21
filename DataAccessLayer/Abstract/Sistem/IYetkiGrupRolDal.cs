using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Abstract.Sistem
{
    public interface IYetkiGrupRolDal : IEntityRepository<YetkiGrupRol>
    {
        List<YetkiGrupRol> GetListByGrupId(int YetkiGrupID);

        List<YetkiGrupRol> GetYetkiRolByYetkiGrupID(int YetkiGrupID);

        int DeleteSoftByYetkiGrupID(int YetkiGrupID);
    }
}