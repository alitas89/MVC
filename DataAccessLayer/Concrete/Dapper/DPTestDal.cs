using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    //Interface içinden interface aldık ve uygulanacak ek bişe kalmadı
    //Eğer ITestDal'a ek yapılar tanımlanırsa o zaman buraya ek yapıları implemente olarak yazmamış gerekir.
    public class DpTestDal:DpEntityRepositoryBase<Test>, ITestDal
    {

    }
}
