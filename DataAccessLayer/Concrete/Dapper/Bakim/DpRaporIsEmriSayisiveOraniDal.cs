using EntityLayer.ComplexTypes.DtoModel.Bakim;
using System;
using Core.DataAccessLayer.Dapper.RepositoryBase;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstract.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;

namespace DataAccessLayer.Concrete.Dapper.Bakim
{
    public class DpRaporIsEmriSayisiveOraniDal : DpEntityRepositoryBase<RaporIsEmriSayisiveOraniTemp>, IRaporIsEmriSayisiveOraniDal
    {
        public RaporIsEmriSayisiveOraniTemp Get(int Id)
        {
            throw new NotImplementedException();
        }

        public List<RaporIsEmriSayisiveOraniTemp> GetList()
        {
            return GetListQuery("select top 1 * from RaporIsEmriSayisiveOrani()", new { });
        }

    }
}
