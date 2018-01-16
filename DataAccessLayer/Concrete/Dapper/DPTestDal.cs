using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer.Dapper;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    //Interface içinden interface aldık ve uygulanacak ek bişe kalmadı
    //Eğer ITestDal'a ek yapılar tanımlanırsa o zaman buraya ek yapıları implemente olarak yazmamış gerekir.
    public class DpTestDal:DpEntityRepositoryBase<Test>, ITestDal
    {
        public List<Test> GetList()
        {
            return GetListQuery($"select * from Test", new { });
        }

        public Test Get(int Id)
        {
            return GetQuery("select * from Test where Id = @Id", new { Id = Id });
        }

        public int Add(Test test)
        {
            return AddQuery("insert into Test(Ip,IsDeleted) values (@Ip,@IsDeleted)", test);
        }

        public int Update(Test test)
        {
            return UpdateQuery("update Test set Ip=@Ip,IsDeleted=@IsDeleted where Id=@Id", test);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Test where Id=@Id", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Test set IsDeleted = 1 where Id=@Id", new { Id });
        }
    }
}
