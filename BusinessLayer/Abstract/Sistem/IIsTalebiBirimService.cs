using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IIsTalebiBirimService
    {
        List<IsTalebiBirim> GetList();

        IsTalebiBirim GetById(int id);

        int Add(IsTalebiBirim ıstalebibirim);

        int Update(IsTalebiBirim ıstalebibirim);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<IsTalebiBirim> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<IsTalebiKullaniciTemp> GetListByIsTipiID(int IsTipiID);

        int AddIsTalebiBirim(int IsTipiID, string arrKullaniciID);
    }

}