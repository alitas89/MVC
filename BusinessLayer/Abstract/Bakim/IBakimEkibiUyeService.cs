using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IBakimEkibiUyeService
    {
        List<BakimEkibiUye> GetList();

        BakimEkibiUye GetById(int id);

        int Add(BakimEkibiUye bakimekibiuye);

        int Update(BakimEkibiUye bakimekibiuye);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<BakimEkibiUye> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");

        int AddBakimEkibiUye(int BakimEkibiID, string arrKaynakID);

        string GetUyeByBakimEkibiID(int BakimEkibiID);
        
    }
}