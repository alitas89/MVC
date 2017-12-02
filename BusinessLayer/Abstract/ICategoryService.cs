using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ICategoryService
    {
        List<Category> GetList();

        Category GetById(int id);

        int Add(Category category);

        int Update(Category category);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}
