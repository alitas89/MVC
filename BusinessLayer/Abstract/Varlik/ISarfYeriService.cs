using System.Collections.Generic;
using EntityLayer.ComplexTypes.DtoModel;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Varlik;

namespace BusinessLayer.Abstract.Varlik
{
    public interface ISarfYeriService
    {
        List<SarfYeri> GetList();

        List<SarfYeri> GetList(int IsletmeID);

        SarfYeri GetById(int id);

        int Add(SarfYeri sarfyeri);

        int Update(SarfYeri sarfyeri);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<SarfYeriDto> GetListDto();

        List<SarfYeri> GetListPagination(PagingParams pagingParams);

        int GetCount(string filterCol = "", string filterVal = "");
    }
}