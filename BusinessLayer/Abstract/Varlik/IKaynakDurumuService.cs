using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IKaynakDurumuService
    {
        List<KaynakDurumu> GetList();

        KaynakDurumu GetById(int id);

        int Add(KaynakDurumu kaynakdurumu);

        int Update(KaynakDurumu kaynakdurumu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<KaynakDurumu> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}