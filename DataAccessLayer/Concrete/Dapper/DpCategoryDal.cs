using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DataAccessLayer;
using Core.DataAccessLayer.Dapper;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.DataAccessLayer.Dapper.RepositoryInterface;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpCategoryDal : DpEntityRepositoryBase<Category>, ICategoryDal
    {
        public List<Category> GetList()
        {
            return GetListQuery("select * from Category", new { });
        }

        public Category Get(int Id)
        {
            return GetQuery("select *  from Category where Id = @Id", new { Id = Id });
        }

        public int Add(Category category)
        {
            return AddQuery("insert Category(Name,Weight,IsDeleted) values (@Name,@Weight,@IsDeleted)", category);
        }

        public int Update(Category category)
        {
            return UpdateQuery("update Category set Name=@Name,Weight=@Weight,IsDeleted=@IsDeleted where CategoryId=@CategoryId", category);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Category where CategoryId=@CategoryId", new { CategoryId = Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Category set IsDeleted = 1 where CategoryId=@CategoryId", new { CategoryId = Id });
        }
    }
}
