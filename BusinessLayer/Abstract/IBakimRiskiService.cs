using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IBakimRiskiService
    {
        BakimRiski GetById(int id);

        int Add(BakimRiski bakimriski);

        int Update(BakimRiski bakimriski);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}