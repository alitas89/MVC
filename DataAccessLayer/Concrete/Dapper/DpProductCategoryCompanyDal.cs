using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete.DatabaseModel;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpProductCategoryCompanyDal : DpMulti3EntityRepositoryBase<Product, Category, Company>, IProductCategoryCompanyDal
    {

    }
}
