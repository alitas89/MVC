using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpProductCategoryCompanyDal : DpMulti3EntityRepositoryBase<Product, Category, Company>, IProductCategoryCompanyDal
    {
        public List<Product> GetProductCategoryCompany()
        {
            string query = @"SELECT      p.*,c.*, s.*
                            FROM            dbo.Product AS p INNER JOIN
                                dbo.Category AS c ON c.CategoryId = p.CategoryId
                            INNER JOIN dbo.Company s ON s.CompanyId = p.CompanyId";

            return GetListMappingQuery(query, "CategoryId");
        }
    }
}
