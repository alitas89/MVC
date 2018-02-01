using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IBakimArizaKoduService
    {
        List<BakimArizaKodu> GetList();

        BakimArizaKodu GetById(int id);

        int Add(BakimArizaKodu bakimarizakodu);

        int Update(BakimArizaKodu bakimarizakodu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<BakimArizaKodu> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}