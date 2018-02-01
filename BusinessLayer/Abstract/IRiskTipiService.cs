using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
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

        List<RiskTipi> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}