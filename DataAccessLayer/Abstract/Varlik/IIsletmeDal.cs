using Core.DataAccessLayer;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IIsletmeDal : IEntityRepository<Isletme>
    {
        bool IsKodDefined(string Kod);
        List<string> AddListWithTransactionBySablon(List<Isletme> listIsletme);
    }
}