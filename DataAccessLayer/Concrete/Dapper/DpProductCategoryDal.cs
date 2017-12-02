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
    public class DpProductCategoryDal : DpMulti2EntityRepositoryBase<Product, Category>, IProductCategoryDal
    {
        public List<Product> GetProductCategory()
        {
            string query = @"SELECT      p.*,c.*
                            FROM            dbo.Product AS p INNER JOIN
                                dbo.Category AS c ON c.CategoryId = p.CategoryId";

            return GetListMappingQuery(query, "CategoryId");
        }
    }
}
