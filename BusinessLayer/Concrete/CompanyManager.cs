using System.Collections.Generic;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class CompanyManager : ICompanyService
    {
        ICompanyDal _companyDal;

        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }

        public List<Company> GetList()
        {
            return _companyDal.GetList();
        }

        public List<Company> GetListPagination(int offset, int limit, string filterCol, string filterVal)
        {
            return _companyDal.GetListPagination(offset, limit, filterCol, filterVal);
        }

        public int GetCount()
        {
            return _companyDal.GetCount();
        }

        public Company GetById(int Id)
        {
            return _companyDal.Get(Id);
        }

        public int Add(Company company)
        {
            return _companyDal.Add(company);
        }
        public int Update(Company company)
        {
            return _companyDal.Update(company);
        }
        public int Delete(int CompanyId)
        {
            return _companyDal.Delete(CompanyId);
        }
        public int DeleteSoft(int CompanyId)
        {
            return _companyDal.DeleteSoft(CompanyId);
        }
    }
}