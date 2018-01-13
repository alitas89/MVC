using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IBakimRiskiService
    {
        List<BakimRiski> GetList();

        BakimRiski GetById(int id);

        int Add(BakimRiski bakimriski);

        int Update(BakimRiski bakimriski);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}