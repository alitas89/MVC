using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules.FluentValidation;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.ValidationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class ConsumptionPlaceManager : IConsumptionPlaceService
    {
        IConsumptionPlaceDal _companyDal;

        public ConsumptionPlaceManager(IConsumptionPlaceDal companyDal)
        {
            _companyDal = companyDal;
        }

        //Verileri çekerken ya cacheden getir yada cache'e al işlemi yapar
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<ConsumptionPlace> GetList()
        {
            return _companyDal.GetList();
        }

        //Verileri çekerken ya cacheden getir yada cache'e al işlemi yapar
        [CacheAspect(typeof(MemoryCacheManager))]
        public ConsumptionPlace GetById(int id)
        {
            return _companyDal.Get(id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(ConsumptionPlaceValidator))]
        public int Add(ConsumptionPlace consumptionplace)
        {
            return _companyDal.Add(consumptionplace);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(ConsumptionPlaceValidator))]
        public int Update(ConsumptionPlace consumptionplace)
        {
            return _companyDal.Update(consumptionplace);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int Delete(int Id)
        {
            return _companyDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int DeleteSoft(int Id)
        {
            return _companyDal.DeleteSoft(Id);
        }
    }
}
