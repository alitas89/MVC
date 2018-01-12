using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IBilgilendirmeTuruService
    {
        BilgilendirmeTuru GetById(int id);

        int Add(BilgilendirmeTuru bilgilendirmeturu);

        int Update(BilgilendirmeTuru bilgilendirmeturu);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}