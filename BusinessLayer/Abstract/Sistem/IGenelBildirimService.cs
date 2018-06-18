using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IGenelBildirimService
    {
        List<GenelBildirim> GetList();

        GenelBildirim GetById(int id);

        int Add(GenelBildirim genelbildirim);

        int Update(GenelBildirim genelbildirim);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<GenelBildirim> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");

        List<GenelBildirim> GetListYeniBildirimByKime(int Kime);

        List<GenelBildirim> GetListByKime(int Kime);
    }
}