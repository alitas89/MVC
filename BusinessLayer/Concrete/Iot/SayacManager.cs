using BusinessLayer.Abstract.Iot;
using DataAccessLayer.Abstract.Iot;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Iot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.ComplexTypes.DtoModel.Iot;

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

        public List<SayacDto> GetListPaginationDtoByModemSeriNo(PagingParams pagingParams, string modemSeriNo)
        {
            return _sayacDal.GetListPaginationDtoByModemSeriNo(pagingParams, modemSeriNo);
        }

        public List<SayacDto> GetListPaginationDto(PagingParams pagingParams)
        {
            return _sayacDal.GetListPaginationDto(pagingParams);
        }

        public int GetCountDtoByModemSeriNo(string modemserino, string filter = "")
        {
            return _sayacDal.GetCountDtoByModemSeriNo(modemserino, filter); ;
        }

        List<SayacDto> ISayacService.GetListPaginationDto(PagingParams pagingParams)
        {
            throw new NotImplementedException();
        }

        List<SayacDto> ISayacService.GetListPaginationDtoByModemSeriNo(PagingParams pagingParams, string modemSeriNo)
        {
            throw new NotImplementedException();
        }
    }
}
