using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IGonderimFormatiService
    {
        GonderimFormati GetById(int id);

        int Add(GonderimFormati gonderimformati);

        int Update(GonderimFormati gonderimformati);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}