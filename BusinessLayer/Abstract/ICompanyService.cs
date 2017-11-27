using System.Collections.Generic;
using EntityLayer.Concrete.DatabaseModel;

namespace BusinessLayer.Abstract
{
    public interface ICompanyService
    {
        List<Company> GetList(int top = 0, string whereQuery = "", object parameters = null);

        Company GetById(int id);

        int Add(Company company);

        int Update(Company company);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}