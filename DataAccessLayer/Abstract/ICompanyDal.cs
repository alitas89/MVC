using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface ICompanyDal : IEntityRepository<Company>
    {
        List<Company> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}