using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Malzeme;
using System.Collections.Generic;

namespace BusinessLayer.Abstract.Malzeme
{
    public interface IMalzemeHareketFisService
    {
        List<MalzemeHareketFis> GetList();

        MalzemeHareketFis GetById(int id);

        int Add(MalzemeHareketFis malzemehareketfis);

        int Update(MalzemeHareketFis malzemehareketfis);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<MalzemeHareketFis> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}
