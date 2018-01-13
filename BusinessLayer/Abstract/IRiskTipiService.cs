using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IRiskTipiService
    {
        List<RiskTipi> GetList();

        RiskTipi GetById(int id);

        int Add(RiskTipi risktipi);

        int Update(RiskTipi risktipi);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}