using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Abstract.Personel
{
    public interface IKaynakSinifiService
    {
        List<KaynakSinifi> GetList();

        KaynakSinifi GetById(int id);

        int Add(KaynakSinifi kaynaksinifi);

        int Update(KaynakSinifi kaynaksinifi);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<KaynakSinifi> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<string> AddListWithTransactionBySablon(List<KaynakSinifi> listKaynakSinifi);
    }
}