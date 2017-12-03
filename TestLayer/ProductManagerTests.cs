using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace TestLayer
{
    [TestFixture]
    public class ProductManagerTests
    {
        [ExpectedException(typeof(ValidationException))]
        [Test]
        public void Product_validation_check()
        {
            Mock<IProductDal> mock1 = new Mock<IProductDal>();
            Mock<IProductCategoryDal> mock2 = new Mock<IProductCategoryDal>();
            Mock<IProductCategoryCompanyDal> mock3 = new Mock<IProductCategoryCompanyDal>();


            ProductManager productManager = 
                new ProductManager(mock1.Object, mock2.Object, mock3.Object);

            productManager.Add(new Product());
        }
    }
}
