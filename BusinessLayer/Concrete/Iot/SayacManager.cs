using BusinessLayer.Abstract.Iot;
using Core.Aspects.Postsharp.AuthorizationAspects;
using DataAccessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.DtoModel.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete.Iot
{
    public class SayacManager : ISayacService
    {
        ISayacDal _sayacDal;

        public SayacManager(ISayacDal sayacDal)
        {
            _sayacDal = sayacDal;
        }

        public int GetCountDto(string filter = "")
        {
            return _sayacDal.GetCountDto(filter);
        }

        [SecuredOperation(Roles = "Admin, IOTRead, SayacRead, SayacLtd")]
        public List<SayacDto> GetListPaginationDtoByModemSeriNo(PagingParams pagingParams, string modemSeriNo)
        {
            return _sayacDal.GetListPaginationDtoByModemSeriNo(pagingParams, modemSeriNo);
        }

        [SecuredOperation(Roles = "Admin, IOTRead, SayacRead, SayacLtd")]
        public List<SayacDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _sayacDal.GetListPaginationDto(pagingParams);
        }

        [SecuredOperation(Roles = "Admin, IOTRead, SayacRead, SayacLtd")]
        public int GetCountDtoByModemSeriNo(string modemserino, string filter = "")
        {
            return _sayacDal.GetCountDtoByModemSeriNo(modemserino, filter); ;
        }
    }
}
