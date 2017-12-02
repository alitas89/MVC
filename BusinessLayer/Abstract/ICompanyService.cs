using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ICompanyService
    {
        List<Company> GetList();

        Company GetById(int id);

        int Add(Company company);

        int Update(Company company);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}