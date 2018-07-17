using Core.DataAccessLayer;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace DataAccessLayer.Abstract.Varlik
{
    public interface IDurusNedeniDal : IEntityRepository<DurusNedeni>
    {
        List<string> AddListWithTransactionBySablon(List<DurusNedeni> listDurusNedeni);
    }
}