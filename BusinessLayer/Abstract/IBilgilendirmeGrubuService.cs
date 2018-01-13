using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IBilgilendirmeGrubuService
    {
        List<BilgilendirmeGrubu> GetList();

        BilgilendirmeGrubu GetById(int id);

        int Add(BilgilendirmeGrubu bilgilendirmegrubu);

        int Update(BilgilendirmeGrubu bilgilendirmegrubu);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}