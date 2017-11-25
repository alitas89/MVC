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

    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        private IProductCategoryDal _productCategoryDal;

        public ProductManager(IProductDal productDal, IProductCategoryDal productCategoryDal)
        {
            _productDal = productDal;
            _productCategoryDal = productCategoryDal;
        }


        
        public List<Product> GetList()
        {
            return _productDal.GetList("select * from Product", new {});
        }

        public List<Product> GetListWithCategory()
        {
            string query = @"SELECT      p.*,c.*
                            FROM            dbo.Product AS p INNER JOIN
                                dbo.Category AS c ON c.CategoryId = p.CategoryId";

            //var x =  _productCategoryDal.GetListMapping(query, (p,c)=> { p.Category = c;
            //    return p;
            //}, new {});

            return _productCategoryDal.GetListMapping(query, "CategoryId");
        }

        public Product GetById(int Id)
        {
            return _productDal.Get("select *  from Product where Id = @Id", new { Id = Id });
        }

        public int Add(Product product)
        {
            return _productDal.Add("insert Product(Name,Color,CategoryId,IsDeleted) values (@Name,@Color,@CategoryId,@IsDeleted)", product);
        }
        public int Update(Product product)
        {
            return _productDal.Update("update Product set Name=@Name,Color=@Color,CategoryId=@CategoryId,IsDeleted=@IsDeleted where Id=@Id", product);
        }
        public int Delete(int Id)
        {
            return _productDal.Delete("delete from Product where Id=@Id", new { Id = Id });
        }
        public int DeleteSoft(int Id)
        {
            return _productDal.Update("update Product set IsDeleted = 1 where Id=@Id", new { Id = Id });
        }
    }
}
