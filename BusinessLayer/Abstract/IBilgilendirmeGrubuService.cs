using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IBilgilendirmeGrubuService
    {
        BilgilendirmeGrubu GetById(int id);

        int Add(BilgilendirmeGrubu bilgilendirmegrubu);

        int Update(BilgilendirmeGrubu bilgilendirmegrubu);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}