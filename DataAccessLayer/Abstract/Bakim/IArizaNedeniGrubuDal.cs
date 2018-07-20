using Core.DataAccessLayer;
using EntityLayer.Concrete.Bakim;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Bakim
{
    public interface IArizaNedeniGrubuDal : IEntityRepository<ArizaNedeniGrubu>
    {
        List<string> AddListWithTransactionBySablon(List<ArizaNedeniGrubu> listArizaNedeniGrubu);
    }
}