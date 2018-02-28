using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;
using System.Collections.Generic;

namespace BusinessLayer.Abstract.Personel
{
    public interface IKaynakService
    {
        List<Kaynak> GetList();

        Kaynak GetById(int id);

        int Add(Kaynak kaynak);

        int Update(Kaynak kaynak);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Kaynak> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}
