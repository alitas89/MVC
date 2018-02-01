using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IBilgilendirmeTuruService
    {
        List<BilgilendirmeTuru> GetList();

        BilgilendirmeTuru GetById(int id);

        int Add(BilgilendirmeTuru bilgilendirmeturu);

        int Update(BilgilendirmeTuru bilgilendirmeturu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<BilgilendirmeTuru> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}