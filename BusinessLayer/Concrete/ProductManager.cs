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

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }


        
        public List<Product> GetList()
        {
            return _productDal.GetList("select * from Product", new {});
        }

        public List<Product> GetListWithCategory()
        {
            string query = @"SELECT dbo.Product.Id, dbo.Product.Name, dbo.Product.Color, 
                            dbo.Category.Name AS Expr1, dbo.Category.Weight
            FROM dbo.Category s INNER JOIN
            dbo.Product ON dbo.Category.Id = dbo.Product.Id";

            //return _productDal.GetListMapping(query,
            //    //(a, s) => { a.Category = s;
            //    //return a;}
            //    new System.Func<Product, Product, Product>()
            //    , new {});
            return  new List<Product>();
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
