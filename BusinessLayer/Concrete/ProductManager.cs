using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{

    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        private IProductCategoryDal _productCategoryDal;
        private IProductCategoryCompanyDal _productCategoryCompanyDal;

        public ProductManager(IProductDal productDal, IProductCategoryDal productCategoryDal,
            IProductCategoryCompanyDal productCategoryCompanyDal)
        {
            _productDal = productDal;
            _productCategoryDal = productCategoryDal;
            _productCategoryCompanyDal = productCategoryCompanyDal;
        }



        public List<Product> GetList()
        {
            return _productDal.GetList("select * from Product", new { });
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

        public List<Product> GetListWithCategoryCompany()
        {
            string query = @"SELECT      p.*,c.*, s.*
                            FROM            dbo.Product AS p INNER JOIN
                                dbo.Category AS c ON c.CategoryId = p.CategoryId
                            INNER JOIN dbo.Company s ON s.CompanyId = p.CompanyId";

            //var x =  _productCategoryDal.GetListMapping(query, (p,c)=> { p.Category = c;
            //    return p;
            //}, new {});

            var x = _productCategoryCompanyDal.GetListMapping(query, "CategoryId");
            return x;
        }

        /// <summary>
        /// Tek class üzerinden mapleme
        /// </summary>
        /// <returns></returns>
        public List<ProductNameColorDto> GetListProductNameColor()
        {
            //Mapleme için konfigürasyon yapılır
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductNameColorDto>());
            var mapper = config.CreateMapper();
            //List<Product> çekilir
            var listProduct = GetList();
            //Mapleme işlemi gerçekleştirilir
            var listProductNameColorDto = mapper.Map<List<Product>, List<ProductNameColorDto>>(listProduct);

            return listProductNameColorDto;
        }

        /// <summary>
        /// 2 class üzerinden mapleme
        /// </summary>
        /// <returns></returns>
        public List<ProductCategoryNamesDto> GetListProductCategoryNames()
        {
            //Mapleme için konfigürasyon yapılır
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<Product, ProductCategoryNamesDto>()
                        .ForMember("CategoryName", opt => opt.MapFrom(src => src.Category.Name));
                    cfg.CreateMap<Product, ProductCategoryNamesDto>()
                                           .ForMember(x=>x.CategoryId, opt => opt.MapFrom(src => src.Category.CategoryId));
                }
                );
            var mapper = config.CreateMapper();
            //List<Product> çekilir
            var listProduct = GetListWithCategory();
            //Mapleme işlemi gerçekleştirilir
            var listProductCategoryNames = mapper.Map<List<Product>, List<ProductCategoryNamesDto>>(listProduct);

            return listProductCategoryNames;
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
