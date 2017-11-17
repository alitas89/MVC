using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete.Dapper;
using Ninject.Modules;
using WebApi.Controllers;

namespace TestLayer
{
    public class NinjectBinds: NinjectModule
    {
        public override void Load()
        {
            Bind<ITestService>().To<TestManager>();
            Bind<ITestDal>().To<DpTestDal>();
        }
    }
}
