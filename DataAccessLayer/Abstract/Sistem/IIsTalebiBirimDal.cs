using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Sistem;
using EntityLayer.Concrete.Sistem;

namespace DataAccessLayer.Abstract.Sistem
{
    public interface IIsTalebiBirimDal : IEntityRepository<IsTalebiBirim>
    {
        List<IsTalebiKullaniciTemp> GetListByIsTipiID(int IsTipiID);

        int AddWithTransaction(int IsTipiID, List<int> listKullaniciID);
    }
}