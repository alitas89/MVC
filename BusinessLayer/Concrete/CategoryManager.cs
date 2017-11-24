using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete.DatabaseModel;

namespace BusinessLayer.Concrete
{

    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetList(int top = 0, string whereQuery = "", object parameters = null)
        {
            string topSql = top == 0 ? "" : "TOP " + top;
            return _categoryDal.GetList($"select {topSql}* from Test" + whereQuery, parameters);
        }

        public Category GetById(int Id)
        {
            return _categoryDal.Get("select *  from Category where Id = @Id", new {Id = Id});
        }

        public int Add(Category category)
        {
            return _categoryDal.Add("insert Category(Name,Weight,IsDeleted) values (@Name,@Weight,@IsDeleted)",
                category);
        }

        public int Update(Category category)
        {
            return _categoryDal.Update(
                "update Category set Name=@Name,Weight=@Weight,IsDeleted=@IsDeleted where Id=@Id", category);
        }

        public int Delete(int Id)
        {
            return _categoryDal.Delete("delete from Category where Id=@Id", new {Id = Id});
        }

        public int DeleteSoft(int Id)
        {
            return _categoryDal.Update("update Category set IsDeleted = 1 where Id=@Id", new {Id = Id});
        }
    }
}