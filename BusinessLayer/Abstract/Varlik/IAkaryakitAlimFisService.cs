using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;
using System.Collections.Generic;

namespace BusinessLayer.Abstract.Varlik
{
    public interface IAkaryakitAlimFisService
    {
        List<AkaryakitAlimFis> GetList();

        AkaryakitAlimFis GetById(int id);

        int Add(AkaryakitAlimFis akaryakitalimfis);

        int Update(AkaryakitAlimFis akaryakitalimfis);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<AkaryakitAlimFis> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}
