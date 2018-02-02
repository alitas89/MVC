using System.Collections.Generic;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract.Genel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Genel;

namespace DataAccessLayer.Concrete.Dapper.Genel
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

        public List<Company> GetListPagination(PagingParams pagingParams)
        {
            string filterQuery = "";
            string orderQuery = "ORDER BY 1";
            if (pagingParams.filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                pagingParams.filterVal = '%' + pagingParams.filterVal + '%';
                filterQuery = $"and {pagingParams.filterCol} like @filterVal";
            }

            if (pagingParams.order.Length != 0)
            {
                var arrOrder = pagingParams.order.Split('~');
                orderQuery = $"ORDER BY {arrOrder[0]} {arrOrder[1]}";
            }

            return GetListQuery($@"SELECT * FROM Company where Silindi=0 {filterQuery} {orderQuery}
                                    OFFSET @offset ROWS FETCH NEXT @limit ROWS ONLY",
                new { pagingParams.filterVal, pagingParams.offset, pagingParams.limit });
        }

        public int GetCount(string filterCol = "", string filterVal = "")
        {
            string where = "";
            if (filterVal.Length != 0)
            {
                //Filtreleme yapılacaktır.
                filterVal = '%' + filterVal + '%';
                where = $" where {filterCol} like @filterVal";
            }

            var strCount = GetScalarQuery($@"SELECT COUNT(*) FROM Company {where}", new { filterVal })+ "";

            int.TryParse(strCount, out int count);
            return count;
        }
    }
}