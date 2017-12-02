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
            return _productDal.GetList();
        }

        public List<Product> GetListWithCategory()
        {
            return _productCategoryDal.GetProductCategory();
        }

        public List<Product> GetListWithCategoryCompany()
        {
            return _productCategoryCompanyDal.GetProductCategoryCompany();
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
            return _productDal.Get(Id);
        }

        public int Add(Product product)
        {
            return _productDal.Add(product);
        }
        public int Update(Product product)
        {
            return _productDal.Update(product);
        }
        public int Delete(int Id)
        {
            return _productDal.Delete(Id);
        }
        public int DeleteSoft(int Id)
        {
            return _productDal.DeleteSoft(Id);
        }
    }
}
