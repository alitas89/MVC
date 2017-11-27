using System.Collections.Generic;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete.DatabaseModel;

namespace BusinessLayer.Concrete
{
    public class CompanyManager : ICompanyService
    {
        ICompanyDal _companyDal;

        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }

        public List<Company> GetList(int top = 0, string whereQuery = "", object parameters = null)
        {
            string topSql = top == 0 ? "" : "TOP " + top;
            return _companyDal.GetList($"select {topSql}* from Test" + whereQuery, parameters);
        }

        public Company GetById(int Id)
        {
            return _companyDal.Get("select *  from Company where Id = @Id", new { Id = Id });
        }

        public int Add(Company company)
        {
            return _companyDal.Add("insert Company(Name,IsDeleted) values (@Name,@IsDeleted)", company);
        }
        public int Update(Company company)
        {
            return _companyDal.Update("update Company set Name=@Name,IsDeleted=@IsDeleted where CompanyId=@CompanyId", company);
        }
        public int Delete(int CompanyId)
        {
            return _companyDal.Delete("delete from Company where CompanyId=@CompanyId", new { CompanyId = CompanyId });
        }
        public int DeleteSoft(int CompanyId)
        {
            return _companyDal.Update("update Company set IsDeleted = 1 where CompanyId=@CompanyId", new { CompanyId = CompanyId });
        }
    }
}