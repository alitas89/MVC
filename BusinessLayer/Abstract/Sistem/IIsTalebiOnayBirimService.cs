using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IIsTalebiOnayBirimService
    {
        List<IsTalebiOnayBirim> GetList();

        IsTalebiOnayBirim GetById(int id);

        int Add(IsTalebiOnayBirim ıstalebionaybirim);

        int Update(IsTalebiOnayBirim ıstalebionaybirim);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<IsTalebiOnayBirim> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");

        List<IsTalebiKullaniciTemp> GetListByIsTipiID(int IsTipiID);

        int AddIsTalebiOnayBirim(int IsTipiID, string arrKullaniciID);
    }
}