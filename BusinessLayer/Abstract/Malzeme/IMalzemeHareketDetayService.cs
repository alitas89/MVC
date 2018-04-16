using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace BusinessLayer.Abstract.Malzeme
{
    public interface IMalzemeHareketDetayService
    {
        List<MalzemeHareketDetay> GetList();

        MalzemeHareketDetay GetById(int id);

        int Add(MalzemeHareketDetay malzemehareketdetay);

        int Update(MalzemeHareketDetay malzemehareketdetay);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<MalzemeHareketDetay> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}
