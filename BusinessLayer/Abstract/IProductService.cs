using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete.DatabaseModel;

namespace BusinessLayer.Abstract
{
    public interface IProductService
    {
        List<Product> GetList();

        List<Product> GetListWithCategory();

        Product GetById(int id);

        int Add(Product product);

        int Update(Product product);

        int Delete(int Id);

        int DeleteSoft(int Id);
    }
}
