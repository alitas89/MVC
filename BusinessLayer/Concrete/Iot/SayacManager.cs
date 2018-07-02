using BusinessLayer.Abstract.Iot;
using DataAccessLayer.Abstract.Iot;
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

        public int GetCount(string filter = "")
        {
            return _sayacDal.GetCount(filter);
        }

        public List<Sayac> GetListByModemSeriNo(string modemSeriNo)
        {
            return _sayacDal.GetListByModemSeriNo(modemSeriNo);
        }

        public List<Sayac> GetListPagination(PagingParams pagingParams)
        {
            return _sayacDal.GetListPagination(pagingParams);
        }
    }
}
