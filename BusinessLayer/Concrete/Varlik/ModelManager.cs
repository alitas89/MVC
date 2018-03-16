using System.Collections.Generic;
using BusinessLayer.Abstract.Varlik;
using Core.Aspects.Postsharp.AuthorizationAspects;
using Core.Aspects.Postsharp.CacheAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract.Varlik;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Concrete.Varlik
{
    public class ModelManager : IModelService
    {
        IModelDal _modelDal;

        public ModelManager(IModelDal modelDal)
        {
            _modelDal = modelDal;
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikRead, ModelRead, ModelLtd")]
        public List<Model> GetList()
        {
            return _modelDal.GetList();
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikRead, ModelRead, ModelLtd")]
        public List<ModelDto> GetListDto()
        {
            return _modelDal.GetListDto();
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, ModelRead, ModelLtd")]
        public Model GetById(int Id)
        {
            return _modelDal.Get(Id);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikCreate, ModelCreate")]
        public int Add(Model model)
        {
            return _modelDal.Add(model);
        }

        //[FluentValidationAspect(typeof(Validator), AspectPriority = 1)]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikUpdate, ModelUpdate")]
        public int Update(Model model)
        {
            return _modelDal.Update(model);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, ModelDelete")]
        public int Delete(int Id)
        {
            return _modelDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [SecuredOperation(Roles = "Admin, VarlikDelete, ModelDelete")]
        public int DeleteSoft(int Id)
        {
            return _modelDal.DeleteSoft(Id);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, ModelRead, ModelLtd")]
        public List<Model> GetListPagination(PagingParams pagingParams)
        {
            return _modelDal.GetListPagination(pagingParams);
        }

        [SecuredOperation(Roles = "Admin, VarlikRead, ModelRead, ModelLtd")]
        public List<ModelDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _modelDal.GetListPaginationDto(pagingParams);
        }

        public int GetCount(string filter = "")
        {
            return _modelDal.GetCount(filter);
        }

        public int GetCountDto(string filter = "")
        {
            return _modelDal.GetCountDto(filter);
        }
    }
}
