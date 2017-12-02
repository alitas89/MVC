using System.Collections.Generic;
using Core.DataAccessLayer.Dapper;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using Core.DataAccessLayer.Dapper.RepositoryInterface;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace DataAccessLayer.Concrete.Dapper
{
    public class DpProductDal : DpEntityRepositoryBase<Product>, IProductDal
    {
        public List<Product> GetList()
        {
            return GetListQuery("select * from Product", new { });
        }

        public Product Get(int Id)
        {
            return GetQuery("select *  from Product where Id = @Id", new { Id });
        }

        public int Add(Product product)
        {
            return AddQuery("insert Product(Name,Color,CategoryId,IsDeleted) values (@Name,@Color,@CategoryId,@IsDeleted)", product);
        }

        public int Update(Product product)
        {
            return UpdateQuery("update Product set Name=@Name,Color=@Color,CategoryId=@CategoryId,IsDeleted=@IsDeleted where Id=@Id", product);
        }

        public int Delete(int Id)
        {
            return DeleteQuery("delete from Product where Id=@Id", new { Id });
        }

        public int DeleteSoft(int Id)
        {
            return UpdateQuery("update Product set IsDeleted = 1 where Id=@Id", new { Id });
        }
    }
}