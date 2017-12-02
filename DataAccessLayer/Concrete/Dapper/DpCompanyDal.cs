using System.Collections.Generic;
using Core.DataAccessLayer.Dapper;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpCompanyDal : DpEntityRepositoryBase<Company>, ICompanyDal
    {
        public List<Company> GetList()
        {
           return GetListQuery($"select * from Test", new {});
        }

        public List<Company> GetListMapping(string query, string splitOn)
        {
            throw new System.NotImplementedException();
        }

        public Company Get(int Id)
        {
            return GetQuery("select *  from Company where Id = @Id", new {Id});
        }

        public int Add(Company company)
        {
            return AddQuery("insert Company(Name,IsDeleted) values (@Name,@IsDeleted)", company);
        }

        public int Update(Company company)
        {
           return UpdateQuery("update Company set Name=@Name,IsDeleted=@IsDeleted where CompanyId=@CompanyId", company);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Company where CompanyId=@CompanyId", new { CompanyId = Id });
        }

        public int DeleteSoft(int Id)
        {
           return UpdateQuery("update Company set IsDeleted = 1 where CompanyId=@CompanyId", new { CompanyId = Id });
        }
    }
}