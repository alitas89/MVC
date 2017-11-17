using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using Moq;
using NUnit.Framework;
using WebApi.App_Start;
using WebApi.Controllers;
using Ninject;

namespace TestLayer
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void IpFourDotItem()
        {
            //Arrange

            var kernel = new StandardKernel(new NinjectBinds());
            var controllerTest = kernel.Get<TestController>();

            controllerTest.Request = new HttpRequestMessage();
            controllerTest.Configuration = new System.Web.Http.HttpConfiguration();

            //Act
            var response = controllerTest.Get(1);

            //Assert
            var fourDotItemForIp = response.Split('.');
            
            Assert.AreEqual(4, fourDotItemForIp.Length);
        }
    }
}
