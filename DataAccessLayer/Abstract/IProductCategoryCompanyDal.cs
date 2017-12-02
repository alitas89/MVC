using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer;
using Core.DataAccessLayer.Dapper.RepositoryInterface;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IProductCategoryCompanyDal
    {
        List<Product> GetProductCategoryCompany();
    }
}
