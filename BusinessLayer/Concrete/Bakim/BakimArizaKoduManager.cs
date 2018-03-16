using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class BakimArizaKoduManager : IBakimArizaKoduService
    {
        IBakimArizaKoduDal _bakimarizakoduDal;

        public BakimArizaKoduManager(IBakimArizaKoduDal bakimarizakoduDal)
        {
            _bakimarizakoduDal = bakimarizakoduDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimArizaKoduRead, BakimArizaKoduLtd")]
        public List<BakimArizaKodu> GetList()
        {
            return _bakimarizakoduDal.GetList();
        }
        [SecuredOperation(Roles = "Admin, BakimArizaKoduRead, BakimArizaKoduLtd")]
        public BakimArizaKodu GetById(int Id)
        {
            return _bakimarizakoduDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimArizaKoduCreate")]
        public int Add(BakimArizaKodu bakimarizakodu)
        {
            return _bakimarizakoduDal.Add(bakimarizakodu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimArizaKoduUpdate")]
        public int Update(BakimArizaKodu bakimarizakodu)
        {
            return _bakimarizakoduDal.Update(bakimarizakodu);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimArizaKoduDelete")]
        public int Delete(int Id)
        {
            return _bakimarizakoduDal.Delete(Id);
        }
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, BakimArizaKoduDelete")]
        public int DeleteSoft(int Id)
        {
            return _bakimarizakoduDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimArizaKoduRead, BakimArizaKoduLtd")]
        public List<BakimArizaKodu> GetListPagination(PagingParams pagingParams)
        {
            return _bakimarizakoduDal.GetListPagination(pagingParams);
        }
        public int GetCount(string filter = "")
        {
            return _bakimarizakoduDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, BakimArizaKoduRead, BakimArizaKoduLtd")]
        public List<BakimArizaKoduDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _bakimarizakoduDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _bakimarizakoduDal.GetCountDto(filter);
        }
    }
}
