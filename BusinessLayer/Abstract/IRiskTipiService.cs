using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IRiskTipiService
    {
        RiskTipi GetById(int id);

        int Add(RiskTipi risktipi);

        int Update(RiskTipi risktipi);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}