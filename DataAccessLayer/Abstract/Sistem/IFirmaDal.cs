using Core.DataAccessLayer;
using EntityLayer.Concrete.Sistem;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Sistem
{
    public interface IFirmaDal : IEntityRepository<Firma>
    {
        List<string> AddListWithTransactionBySablon(List<Firma> listFirma);
    }
}