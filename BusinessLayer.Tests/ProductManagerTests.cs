﻿using System;
using AutoMapper;
using BusinessLayer.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using FluentValidation;
using Moq;

namespace BusinessLayer.Tests
{
    [TestClass]
    public class ProductManagerTests
    {
        [ExpectedException(typeof(ValidationException))]
        [TestMethod]
        public void Product_validation_check()
        {
            Mock<IProductDal> mock1 = new Mock<IProductDal>();
            Mock<IProductCategoryDal> mock2 = new Mock<IProductCategoryDal>();
            Mock<IProductCategoryCompanyDal> mock3 = new Mock<IProductCategoryCompanyDal>();
            Mock<IMapper> mock4 = new Mock<IMapper>();

            ProductManager productManager =
                new ProductManager(mock1.Object, mock2.Object, mock3.Object, mock4.Object);

            productManager.Add(new Product());
        }
    }
}
