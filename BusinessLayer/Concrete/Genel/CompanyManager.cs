using System.Collections.Generic;
using BusinessLayer.Abstract.Genel;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace BusinessLayer.Concrete.Genel
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

        public List<Company> GetListPagination(PagingParams pagingParams)
        {
            return _companyDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _companyDal.GetCount(filter);
        }
    }
}