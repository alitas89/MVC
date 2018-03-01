using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Personel;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IKaynakTipiService
    {
        List<KaynakTipi> GetList();

        KaynakTipi GetById(int id);

        int Add(KaynakTipi kaynaktipi);

        int Update(KaynakTipi kaynaktipi);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<KaynakTipi> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}