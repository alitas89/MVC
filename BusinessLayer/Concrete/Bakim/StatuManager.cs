using System.Collections.Generic;
using BusinessLayer.Abstract.Bakim;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;

namespace BusinessLayer.Concrete.Bakim
{
    public class StatuManager : IStatuService
    {
        IStatuDal _statuDal;

        public StatuManager(IStatuDal statuDal)
        {
            _statuDal = statuDal;
        }

        
        [SecuredOperation(Roles = "Admin, BakimRead, StatuRead, StatuLtd")]
        public List<Statu> GetList()
        {
            return _statuDal.GetList();
        }

        [SecuredOperation(Roles = "Admin, BakimRead, StatuRead, StatuLtd")]
        public Statu GetById(int Id)
        {
            return _statuDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimCreate, StatuCreate")]
        public int Add(Statu statu)
        {
            return _statuDal.Add(statu);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        
        [SecuredOperation(Roles = "Admin, BakimUpdate, StatuUpdate")]
        public int Update(Statu statu)
        {
            return _statuDal.Update(statu);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, StatuDelete")]
        public int Delete(int Id)
        {
            return _statuDal.Delete(Id);
        }

        
        [SecuredOperation(Roles = "Admin, BakimDelete, StatuDelete")]
        public int DeleteSoft(int Id)
        {
            return _statuDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, StatuRead, StatuLtd")]
        public List<Statu> GetListPagination(PagingParams pagingParams)
        {
            return _statuDal.GetListPagination(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _statuDal.GetCount(filter);
        }

        [SecuredOperation(Roles = "Admin, BakimRead, StatuRead, StatuLtd")]
        public List<StatuDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _statuDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDto(string filter = "")
        {
            return _statuDal.GetCountDto(filter);
        }

        public List<string> AddListWithTransactionBySablon(List<Statu> listStatu)
        {
            return _statuDal.AddListWithTransactionBySablon(listStatu);
        }
    }
}