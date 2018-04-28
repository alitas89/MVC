using EntityLayer.ComplexTypes.DtoModel.Bakim;
using EntityLayer.ComplexTypes.ParameterModel;
using EntityLayer.Concrete.Bakim;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract.Bakim
{
    public interface IIsTipiEmirTuruService
    {
        List<IsTipiEmirTuru> GetList();

        IsTipiEmirTuru GetById(int id);

        int Add(IsTipiEmirTuru ıstipiemirturu);

        int Update(IsTipiEmirTuru ıstipiemirturu);

        int Delete(int Id);

        int DeleteSoft(int Id);

        List<IsTipiEmirTuru> GetListPagination(PagingParams pagingParams);

        int GetCount(string filter = "");

        List<IsTipiEmirTuruDto> GetListPaginationDto(int isTipiID, PagingParams pagingParams);

        int GetCountDto(int isTipiID, string filter = "");

        int AddIsTipiDetay(IsTipiTemp isTipiTemp);

        int UpdateIsTipiDetay(IsTipiTemp isTipiTemp);
    }
}
