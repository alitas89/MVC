using System.Collections.Generic;
using Core.DataAccessLayer;
using EntityLayer.ComplexTypes.DtoModel.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;

namespace DataAccessLayer.Abstract.Iot
{
    public interface IAlarmDal : IEntityRepository<Alarm>
    {
        int AddWithTransaction(Alarm alarm, List<AlarmKosul> listAlarmKosul);

        int UpdateWithTransaction(Alarm alarm, List<AlarmKosul> listAlarmKosul);

        List<AlarmDto> GetListPaginationDto(PagingParams pagingParams);
    }
}