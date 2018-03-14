using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IKaynakTuruService
    {
        List<KaynakTuru> GetList();

        KaynakTuru GetById(int id);

        int Add(KaynakTuru kaynakturu);

        int Update(KaynakTuru kaynakturu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<KaynakTuru> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}