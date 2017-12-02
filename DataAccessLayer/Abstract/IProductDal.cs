﻿using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.Concrete;

namespace DataAccessLayer.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {

    }
}