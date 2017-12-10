using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules.FluentValidation;
using Core.Aspects.Postsharp.CacheAspects;
using Core.Aspects.Postsharp.ExceptionAspects;
using Core.Aspects.Postsharp.ValidationAspects;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class ConsumptionPlaceManager : IConsumptionPlaceService
    {
        IConsumptionPlaceDal _consumptionPlaceDal;

        public ConsumptionPlaceManager(IConsumptionPlaceDal consumptionPlaceDal)
        {
            _consumptionPlaceDal = consumptionPlaceDal;
        }

        //Verileri çekerken ya cacheden getir yada cache'e al işlemi yapar
        [CacheAspect(typeof(MemoryCacheManager))]
        public List<ConsumptionPlace> GetList()
        {
            return _consumptionPlaceDal.GetList();
        }

        //Verileri çekerken ya cacheden getir yada cache'e al işlemi yapar
        [CacheAspect(typeof(MemoryCacheManager))]
        public ConsumptionPlace GetById(int id)
        {
            return _consumptionPlaceDal.Get(id);
        }
        
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(ConsumptionPlaceValidator), AspectPriority = 1)]
        public int Add(ConsumptionPlace consumptionplace)
        {
            return _consumptionPlaceDal.Add(consumptionplace);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        [FluentValidationAspect(typeof(ConsumptionPlaceValidator))]
        public int Update(ConsumptionPlace consumptionplace)
        {
            return _consumptionPlaceDal.Update(consumptionplace);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int Delete(int Id)
        {
            return _consumptionPlaceDal.Delete(Id);
        }

        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public int DeleteSoft(int Id)
        {
            return _consumptionPlaceDal.DeleteSoft(Id);
        }
    }
}
