using System.Collections.Generic;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IConsumptionPlaceService
    {
            List<ConsumptionPlace> GetList();

            ConsumptionPlace GetById(int id);

            int Add(ConsumptionPlace consumptionplace);

            int Update(ConsumptionPlace consumptionplace);

            int Delete(int Id);

            int DeleteSoft(int Id);
    }
}