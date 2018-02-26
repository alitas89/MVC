using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IZimmetTransferDetayService
    {
        List<ZimmetTransferDetay> GetList();

        ZimmetTransferDetay GetById(int id);

        int Add(ZimmetTransferDetay zimmettransferdetay);

        int Update(ZimmetTransferDetay zimmettransferdetay);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<ZimmetTransferDetay> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}