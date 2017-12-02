using System;
using System.Collections.Generic;
using Core.DataAccessLayer;
using Core.DataAccessLayer.Dapper.RepositoryInterface;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IProductCategoryDal
    {
        List<Product> GetProductCategory();
    }
}