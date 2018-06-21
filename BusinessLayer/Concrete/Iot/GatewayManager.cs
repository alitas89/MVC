using BusinessLayer.Abstract.Iot;
using Core.Aspects.Postsharp.AuthorizationAspects;
using DataAccessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.DtoModel.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Concrete.Iot
{
    public class GatewayManager : IGatewayService
    {
        IGatewayDal _gatewayDal;

        public GatewayManager(IGatewayDal gatewayDal)
        {
            _gatewayDal = gatewayDal;
        }

        [SecuredOperation(Roles = "Admin, IOTRead, GatewayRead, GatewayLtd")]
        public Gateway GetById(int id)
        {
            return _gatewayDal.Get(id);
        }

        public int GetCountDto(string filter = "")
        {
            return _gatewayDal.GetCountDto();
        }

        [SecuredOperation(Roles = "Admin, IOTRead, GatewayRead, GatewayLtd")]
        public List<GatewayDto> GetListDto()
        {
            return _gatewayDal.GetListDto();
        }

        [SecuredOperation(Roles = "Admin, IOTRead, GatewayRead, GatewayLtd")]
        public List<GatewayDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _gatewayDal.GetListPaginationDto(pagingParams);
        }
    }
}
