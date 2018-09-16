using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Sistem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract.Sistem
{
    public interface IDosyaService
    {
        List<Dosya> GetList();

        Dosya GetById(int id);

        int Add(Dosya dosya);

        int Update(Dosya dosya);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<Dosya> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter="");

        List<Dosya> GetListByBagliID(int id);
    }


}
