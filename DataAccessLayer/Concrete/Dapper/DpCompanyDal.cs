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
            return GetListQuery("select * from Company where Silindi=0", new { });
        }

        public Company Get(int Id)
        {
            return GetQuery("select * from Company where CompanyId= @Id and Silindi=0", new { Id });
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
            return DeleteQuery("delete from Company where CompanyId=@Id ", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Company set Silindi = 1 where CompanyId=@Id", new { Id });
        }
    }
}