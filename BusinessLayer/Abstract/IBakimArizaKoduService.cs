using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IBakimArizaKoduService
    {
        BakimArizaKodu GetById(int id);

        int Add(BakimArizaKodu bakimarizakodu);

        int Update(BakimArizaKodu bakimarizakodu);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}