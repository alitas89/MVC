using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.Concrete;

namespace BusinessLayer.Mappings.AutoMapper.Profiles
{
    public class BusinessProfile: Profile
    {
        public BusinessProfile()
        {
            //CreateMap<Product, ProductCategoryNamesDto>();
            //CreateMap<Product, Product>();
        }
    }
}
