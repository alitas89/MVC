using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{

    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public List<Category> GetList()
        {
            return _categoryDal.GetList();
        }

        public Category GetById(int Id)
        {
            return _categoryDal.Get(Id);
        }

        public int Add(Category category)
        {
            return _categoryDal.Add(category);
        }

        public int Update(Category category)
        {
            return _categoryDal.Update(category);
        }

        public int Delete(int Id)
        {
            return _categoryDal.Delete(Id);
        }

        public int DeleteSoft(int Id)
        {
            return _categoryDal.DeleteSoft(Id);
        }
    }
}