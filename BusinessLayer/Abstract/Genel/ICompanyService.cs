using System.Collections.Generic;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Abstract.Genel
{
    public interface ICompanyService
    {
        List<Company> GetList();

        Company GetById(int id);

        int Add(Company company);

        int Update(Company company);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Company> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");
    }
}