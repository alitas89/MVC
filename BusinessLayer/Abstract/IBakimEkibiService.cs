using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IBakimEkibiService
    {
        BakimEkibi GetById(int id);

        int Add(BakimEkibi bakimekibi);

        int Update(BakimEkibi bakimekibi);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}