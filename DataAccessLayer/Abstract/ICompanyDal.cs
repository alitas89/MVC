using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface ICompanyDal : IEntityRepository<Company>
    {
        List<Company> GetListPagination(int offset, int limit, string filterCol, string filterVal);

        int GetCount();
    }
}