using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Abstract.Personel
{
    public interface IEgitimService
    {
        List<Egitim> GetList();

        Egitim GetById(int id);

        int Add(Egitim egitim);

        int Update(Egitim egitim);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Egitim> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}